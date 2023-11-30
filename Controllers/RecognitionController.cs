using System;
using FacialRecognition.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FacialRecognition.Controllers
{
    [Route("Recognition")]
    public class RecognitionController : Controller
    {
        private readonly IAwsRekognitionService awsRekognitionService;

        public RecognitionController(IAwsRekognitionService awsRekognitionService)
        {
            this.awsRekognitionService = awsRekognitionService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterImage(IFormFile file)
        {
            try
            {
                await awsRekognitionService.RegisterImage(file);
                return Ok($"Imagem {file.FileName} registrada com sucesso");
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Compare")]
        public async Task<IActionResult> Compare(IFormFile file)
        {
            try
            {
                var result = await awsRekognitionService.CompareFacesAsync(file);
                return Ok(result);
            }
            catch(System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
