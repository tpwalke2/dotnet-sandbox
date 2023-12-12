using System;
using System.Runtime.Serialization;

namespace APIBoilerplate.Plumbing.Validation;

/// <summary>
/// Thrown when arguments to function are invalid
/// </summary>
[Serializable]
public sealed class InvalidArgumentsException : Exception
{
    public InvalidArgumentsException()
    {
    }

    public InvalidArgumentsException(string message) : base(message)
    {
    }

    [Obsolete("Obsolete")]
    private InvalidArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}