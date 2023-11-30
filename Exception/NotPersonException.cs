using System;

namespace FacialRecognition.Exception
{
    public class NotPersonException : System.Exception
    {
        public NotPersonException(string? message) : base(message)
        {
        }

    }
}
