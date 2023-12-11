using System;

namespace APIBoilerplate.Authentication.Commands;

public sealed record LoginResponse
{
    public string Token { get; init; }
    public DateTime Expiration { get; init; }
}