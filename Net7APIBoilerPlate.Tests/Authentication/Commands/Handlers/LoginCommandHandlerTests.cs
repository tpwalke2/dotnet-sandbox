using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Net7APIBoilerplate.Authentication.Commands;
using Net7APIBoilerplate.Authentication.Commands.Handlers;
using Net7APIBoilerplate.Authentication.Data;
using Net7APIBoilerplate.Authentication.Helpers;
using Net7APIBoilerplate.ConfigurationModels;
using Net7APIBoilerPlate.Tests.Plumbing.UnitTesting;
using NUnit.Framework;
using System.Security.Authentication;

namespace Net7APIBoilerPlate.Tests.Authentication.Commands.Handlers;

[TestFixture]
public class LoginCommandHandlerTests : UnitTestFixture<LoginCommandHandler>
{
    private Mock<IUserManager> _userManager;
    private JwtConfig _jwtConfig;
    private LoginCommand _command;
    private ApplicationUser _user;

    [SetUp]
    public void Setup()
    {
        _userManager = Dependency<IUserManager>();

        _command = new LoginCommand()
        {
            Username = "User1",
            Password = "Password123!"
        };

        _user = new ApplicationUser()
        {
            UserName = "User1"
        };
    }

    protected override void PreUnderTestResolve(IServiceCollection services)
    {
        _jwtConfig = new JwtConfig();
        services.TryAddScoped(_ => _jwtConfig);
    }

    [Test]
    public void ShouldThrowExceptionIfUserNotFound()
    {
        _userManager
            .Setup(x => x.FindByNameAsync("User1"))
            .ReturnsAsync(() => default);
        Assert.ThrowsAsync<InvalidCredentialException>(async () => await UnderTest.Handle(_command));
    }
        
    [Test]
    public void ShouldThrowExceptionIfInvalidPassword()
    {
        _userManager
            .Setup(x => x.FindByNameAsync("User1"))
            .ReturnsAsync(() => _user);

        _userManager
            .Setup(x => x.CheckPasswordAsync(_user, "Password123!"))
            .ReturnsAsync(false);
            
        Assert.ThrowsAsync<InvalidCredentialException>(async () => await UnderTest.Handle(_command));
    }
}