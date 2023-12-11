using Microsoft.AspNetCore.Identity;
using Net7APIBoilerplate.Authentication.Commands;
using Net7APIBoilerplate.Authentication.Commands.Handlers;
using Net7APIBoilerplate.Authentication.Data;
using Net7APIBoilerplate.Authentication.Helpers;
using Net7APIBoilerplate.Plumbing.Validation;
using Net7APIBoilerPlate.Tests.Plumbing.UnitTesting;
using NSubstitute;
using System.Threading.Tasks;
using Xunit;

namespace Net7APIBoilerPlate.Tests.Authentication.Commands.Handlers;

public class RegisterUserCommandHandlerTests : UnitTestFixture<RegisterUserCommandHandler>
{
    private readonly IUserManager _userManager;
    private readonly RegisterUserCommand _command;
    private readonly ApplicationUser _user;

    public RegisterUserCommandHandlerTests()
    {
        _userManager = Dependency<IUserManager>();

        _command = new RegisterUserCommand
        {
            Username = "User1",
            Password = "Password123!",
            Email    = "user1@example.com"
        };

        _user = new ApplicationUser
        {
            UserName = "User1"
        };
    }

    [Fact]
    public async Task ShouldThrowExceptionIfUserExists()
    {
        _userManager
            .FindByNameAsync("User1")
            .Returns(_user);

        await Assert.ThrowsAsync<InvalidArgumentsException>(() => UnderTest.Handle(_command));
    }

    [Fact]
    public async Task ShouldCreateUser()
    {
        ApplicationUser createdUser = null;

        _userManager
            .FindByNameAsync("User1")
            .Returns(default(ApplicationUser));

        _userManager
            .CreateAsync(
                Arg.Do<ApplicationUser>(user => createdUser = user),
                "Password123!")
            .Returns(IdentityResult.Success);

        var result = await UnderTest.Handle(_command);

        Assert.True(result.Success);
        Assert.NotNull(createdUser);
        Assert.Equal("user1@example.com", createdUser?.Email);
        Assert.Equal("User1", createdUser?.UserName);
    }

    [Fact]
    public async Task ShouldReturnErrorMessageIfCreateFails()
    {
        _userManager
            .FindByNameAsync("User1")
            .Returns(default(ApplicationUser));

        _userManager
            .CreateAsync(
                Arg.Any<ApplicationUser>(),
                "Password123!")
            .Returns(IdentityResult.Failed());

        var result = await UnderTest.Handle(_command);

        Assert.False(result.Success);
        Assert.NotNull(result.ErrorMessage);
        Assert.NotEmpty(result.ErrorMessage);
    }
}