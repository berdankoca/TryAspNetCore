using System;

namespace TryAspNetCore.Core
{
    public class BaseException : Exception
    {
        public BaseException()
        {
        }

        public BaseException(string message)
            : base(message)
        {

        }
    }
}