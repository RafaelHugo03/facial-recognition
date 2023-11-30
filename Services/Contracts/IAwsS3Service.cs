using System;

namespace FacialRecognition.Services.Contracts
{
    public interface IAwsS3Service
    {
        Task UploadToS3(IFormFile file);
    }
}
