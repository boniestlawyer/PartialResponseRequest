using System;

namespace PartialResponseRequest.Core.TokenReaders
{
    public class UnexpectedCharException : Exception
    {
        public UnexpectedCharException(int index, char c) : base($"Unexpected char `${c}` at ${index} position")
        {
        }
    }
}
