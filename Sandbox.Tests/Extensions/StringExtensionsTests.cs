using Sandbox.Extensions;
using Xunit;

namespace Sandbox.Tests.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("4111111111111111", true, 1)]
    [InlineData("79927398710", false, 3)]
    [InlineData("49927398716", true, 6)]
    [InlineData("1234567812345678", false, 0)]
    [InlineData("374652346956782346957823694857692364857368475368", true, 8)]
    [InlineData("0", true, 0)]
    [InlineData("59", true, 9)]
    [InlineData("00", true, 0)]
    public void LuhnValidationTester(string input, bool expectedIsValid, int expectedCheckDigit)
    {
        var result = input.ValidateLuhn();

        Assert.Multiple(() =>
            {
                Assert.Equal(expectedIsValid, result.IsValid);
                Assert.Equal(expectedCheckDigit, result.CheckDigit);
            }
        );
    }
}