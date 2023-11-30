using System;

namespace FacialRecognition.DTOs
{
    public class CompareResponse
    {
        public string ImageName { get; set; }
        public float Similarity { get; set; }
    }
}
