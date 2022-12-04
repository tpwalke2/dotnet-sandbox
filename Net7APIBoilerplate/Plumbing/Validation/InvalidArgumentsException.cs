using System;

namespace Net7APIBoilerplate.Plumbing.Validation;

/// <summary>
/// Thrown when arguments to function are invalid
/// </summary>
public class InvalidArgumentsException: Exception
{
    public InvalidArgumentsException()
    {}
    public InvalidArgumentsException(string message): base(message) {}
}