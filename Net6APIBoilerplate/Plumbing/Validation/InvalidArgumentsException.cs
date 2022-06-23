using System;

namespace Net6APIBoilerplate.Plumbing.Validation;

/// <summary>
/// Thrown when arguments to function are invalid
/// </summary>
public class InvalidArgumentsException: Exception
{
    public InvalidArgumentsException()
    {}
    public InvalidArgumentsException(string message): base(message) {}
}