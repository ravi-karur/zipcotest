using System;

namespace CustomerApi.Domain.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
            : base(message)
        {
        }
    }
}
