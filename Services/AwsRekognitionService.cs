using Amazon.Rekognition.Model;
using FacialRecognition.DTOs;
using FacialRecognition.Exception;
using FacialRecognition.Factory;
using FacialRecognition.Services.Contracts;

namespace FacialRecognition.Services
{
    public class AwsRekognitionService : IAwsRekognitionService
    {
        private readonly IAwsS3Service awsS3Service;
        private readonly string COLLECTION_ID = "my-photos";
        private readonly string BUCKET_NAME = "facial-recognition-test";

        public AwsRekognitionService(IAwsS3Service awsS3Service)
        {
            this.awsS3Service = awsS3Service;
        }


        public async Task<CompareResponse> CompareFacesAsync(IFormFile file)
        {
            var client = AwsFactory.GetAmazonRekognitionClient();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                 
                var request = new SearchFacesByImageRequest
                {
                    CollectionId = this.COLLECTION_ID,
                    Image = new Image { Bytes = stream},
                    FaceMatchThreshold = 90F,
                    MaxFaces = 5
                };

                var response = await client.SearchFacesByImageAsync(request);

                if(response.FaceMatches.Count < 1) throw new NothingFacesMatchesException("Rosto não cadastrado");

                return new CompareResponse
                {
                    ImageName = response.FaceMatches[0].Face.ExternalImageId,
                    Similarity = response.FaceMatches[0].Similarity,
                };
            }
        }

        public async Task RegisterImage(IFormFile file)
        {
            if(!(await IsFace(file))) 
                throw new NotPersonException("Imagem inserida não é de um rosto");
            
            try
            {
                await awsS3Service.UploadToS3(file);
                
                var client = AwsFactory.GetAmazonRekognitionClient();

                await client.CreateCollectionAsync(new CreateCollectionRequest{CollectionId = this.COLLECTION_ID});

                using(var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);

                    var image = new Image
                    {
                        S3Object = new S3Object
                        {
                            Bucket = this.BUCKET_NAME,
                            Name = file.FileName
                        }
                    };

                    var indexFacesRequest = new IndexFacesRequest
                    {
                        Image = image,
                        CollectionId = this.COLLECTION_ID,
                        ExternalImageId = image.S3Object.Name,
                        DetectionAttributes = new List<string>() {"ALL"}
                    };

                    var response = await client.IndexFacesAsync(indexFacesRequest);
                }
            }
            catch(System.Exception e)
            {
                throw e;
            }
        }

        private async Task<bool> IsFace(IFormFile file)
        {
            var client = AwsFactory.GetAmazonRekognitionClient();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var request = new DetectFacesRequest
                {
                    Image = new Image
                    {
                        Bytes = stream
                    }
                };
                
                var result = await client.DetectFacesAsync(request);

                return result.FaceDetails[0].Confidence > 95;
            }
        }

    }
}
