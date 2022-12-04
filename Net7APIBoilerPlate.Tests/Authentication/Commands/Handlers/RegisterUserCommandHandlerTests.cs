using Microsoft.AspNetCore.Identity;
using Moq;
using Net7APIBoilerplate.Authentication.Commands;
using Net7APIBoilerplate.Authentication.Commands.Handlers;
using Net7APIBoilerplate.Authentication.Data;
using Net7APIBoilerplate.Authentication.Helpers;
using Net7APIBoilerplate.Plumbing.Validation;
using Net7APIBoilerPlate.Tests.Plumbing.UnitTesting;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Net7APIBoilerPlate.Tests.Authentication.Commands.Handlers;

[TestFixture]
public class RegisterUserCommandHandlerTests : UnitTestFixture<RegisterUserCommandHandler>
{
    private Mock<IUserManager> _userManager;
    private RegisterUserCommand _command;
    private ApplicationUser _user;
        
    [SetUp]
    public void Setup()
    {
        _userManager = Dependency<IUserManager>();

        _command = new RegisterUserCommand()
        {
            Username = "User1",
            Password = "Password123!",
            Email    = "user1@example.com"
        };

        _user = new ApplicationUser()
        {
            UserName = "User1"
        };
    }

    [Test]
    public void ShouldThrowExceptionIfUserExists()
    {
        _userManager
            .Setup(x => x.FindByNameAsync("User1"))
            .ReturnsAsync(_user);

        Assert.ThrowsAsync<InvalidArgumentsException>(async () => await UnderTest.Handle(_command));
    }

    [Test]
    public async Task ShouldCreateUser()
    {
        ApplicationUser createdUser = null;

        _userManager
            .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Password123!"))
            .Callback<ApplicationUser, string>((user, password) =>
            {
                createdUser = user;
                Assert.That(password, Is.EqualTo("Password123!"));
            })
            .ReturnsAsync(IdentityResult.Success);

        var result = await UnderTest.Handle(_command);
            
        Assert.That(result.Success, Is.True);
        Assert.That(createdUser, Is.Not.Null);
        Assert.That(createdUser?.Email, Is.EqualTo("user1@example.com"));
        Assert.That(createdUser?.UserName, Is.EqualTo("User1"));
    }

    [Test]
    public async Task ShouldReturnErrorMessageIfCreateFails([Values] bool flag)
    {
        _userManager
            .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), "Password123!"))
            .ReturnsAsync(IdentityResult.Failed());

        var result = await UnderTest.Handle(_command);
            
        Assert.That(result.Success, Is.False);
        Assert.That(result.ErrorMessage, Is.Not.Null.Or.Empty);
    }
}