using System;

namespace FacialRecognition.Exception
{
    public class NothingFacesMatchesException : System.Exception
    {
        public NothingFacesMatchesException(string? message) : base(message)
        {
        }

    }
}
