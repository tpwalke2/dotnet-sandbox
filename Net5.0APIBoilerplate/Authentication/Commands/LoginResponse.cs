using System;

namespace Net5._0APIBoilerplate.Authentication.Commands
{
    public sealed record LoginResponse
    {
        public string Token { get; init; }
        public DateTime Expiration { get; init; }
    }
}
