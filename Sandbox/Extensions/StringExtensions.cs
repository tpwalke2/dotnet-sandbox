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
        // skip last character by starting at 2 and walking backwards
        for (var i = 2; i <= input.Length; i++) {
            // walk backwards through string
            var index        = input.Length - i;
            // multiply by 2 if even index; identity if odd index
            var multiplier   = i % 2 == 0 ? 2 : 1;
            // assume ascii digits and subtract 48 from value of character
            var currentValue = (input[index] - 48) * multiplier;
            // cast out the 9s if appropriate and accumulate sum
            sum += currentValue > 9
                ? currentValue - 9
                : currentValue;
        }

        // perform mod 10 calculation
        return (10 - sum % 10) % 10;
    }
}

public sealed record LuhnValidationResult(bool IsValid, int CheckDigit);