using System;
using Amazon.Rekognition.Model;
using FacialRecognition.DTOs;

namespace FacialRecognition.Services.Contracts
{
    public interface IAwsRekognitionService
    {
        Task RegisterImage(IFormFile file);
        Task<CompareResponse> CompareFacesAsync(IFormFile file);
    }
}
