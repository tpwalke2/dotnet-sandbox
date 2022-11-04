using NUnit.Framework;
using Sandbox.Extensions;

namespace Sandbox.Tests.Extensions;

[TestFixture]
public class StringExtensionsTests
{
    [TestCase("4111111111111111", true, 1)]
    [TestCase("79927398710", false, 3)]
    public void LuhnValidationTester(string input, bool expectedIsValid, int expectedCheckDigit)
    {
        var result = input.ValidateLuhn();
        
        Assert.That(result.IsValid, Is.EqualTo(expectedIsValid));
        Assert.That(result.CheckDigit, Is.EqualTo(expectedCheckDigit));
    }
}