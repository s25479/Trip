using System;

namespace GakkoHorizontalSlice.Exceptions;

public class ValidationException : Exception
{
    public ValidationException(string message)
        : base(message)
    {
    }
}
