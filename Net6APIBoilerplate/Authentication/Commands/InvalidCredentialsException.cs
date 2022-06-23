using System;

namespace Net6APIBoilerplate.Authentication.Commands;

/// <summary>
/// Thrown when an authentication operation cannot complete due to invalid credentials
/// </summary>
public class InvalidCredentialsException: Exception
{
}