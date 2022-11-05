namespace Sandbox.Extensions;

public static class StringExtensions
{
    public static LuhnValidationResult ValidateLuhn(this string input)
    {
        var checkDigit = input.GetLuhnCheckDigit();

        return new LuhnValidationResult(int.Parse(input[^1..]) == checkDigit, checkDigit);
    }
    
    private static int GetLuhnCheckDigit(this string input) {
        var sum = 0;
        for (var i = 2; i <= input.Length; i++) {
            var index        = input.Length - i;
            var multiplier   = i % 2 == 0 ? 2 : 1;
            var currentValue = (input[index] - 48) * multiplier;
            sum += currentValue % 10;
            if (currentValue >= 10) sum++;
        }

        return (10 - sum % 10) % 10;
    }
}

public sealed record LuhnValidationResult(bool IsValid, int CheckDigit);