using System;
using System.Runtime.Serialization;

namespace Net7APIBoilerplate.Plumbing.Validation;

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

    private InvalidArgumentsException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}