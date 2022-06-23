using System;

namespace Net6APIBoilerplate.Authentication.Commands;

public sealed record LoginResponse
{
    public string Token { get; init; }
    public DateTime Expiration { get; init; }
}