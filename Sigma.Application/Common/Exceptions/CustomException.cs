using System;

namespace Sigma.Application.Common.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode { get; set; }

        public CustomException(string message) : base(message)
        {
        }

        public CustomException(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}