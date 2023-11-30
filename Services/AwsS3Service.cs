using System;
using Amazon.S3;
using Amazon.S3.Model;
using FacialRecognition.Factory;
using FacialRecognition.Services.Contracts;

namespace FacialRecognition.Services
{
    public class AwsS3Service : IAwsS3Service
    {
        public async Task UploadToS3(IFormFile file)
        {
            var client = AwsFactory.GetAmazonS3Client();

            using(var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                var request = new PutObjectRequest
                {
                    Key = file.FileName,
                    BucketName = "facial-recognition-test",
                    InputStream = stream
                };

                await client.PutObjectAsync(request);
            }
        }

    }
}
