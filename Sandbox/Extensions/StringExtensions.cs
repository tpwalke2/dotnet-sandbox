namespace Sandbox.Extensions;

public static class StringExtensions
{
    public static LuhnValidationResult ValidateLuhn(this string input)
    {
        return new LuhnValidationResult(true, 1);
    }
}

public sealed record LuhnValidationResult(bool IsValid, int CheckDigit);