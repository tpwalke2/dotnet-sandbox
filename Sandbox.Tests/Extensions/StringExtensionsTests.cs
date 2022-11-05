using NUnit.Framework;
using Sandbox.Extensions;

namespace Sandbox.Tests.Extensions;

[TestFixture]
public class StringExtensionsTests
{
    [TestCase("4111111111111111", true, 1)]
    [TestCase("79927398710", false, 3)]
    [TestCase("49927398716", true, 6)]
    [TestCase("1234567812345678", false, 0)]
    [TestCase("374652346956782346957823694857692364857368475368", true, 8)]
    [TestCase("0", true, 0)]
    [TestCase("59", true, 9)]
    [TestCase("00", true, 0)]
    public void LuhnValidationTester(string input, bool expectedIsValid, int expectedCheckDigit)
    {
        var result = input.ValidateLuhn();

        Assert.Multiple(() =>
            {
                Assert.That(result.IsValid, Is.EqualTo(expectedIsValid));
                Assert.That(result.CheckDigit, Is.EqualTo(expectedCheckDigit));
            }
        );
    }
}