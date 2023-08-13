using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Net7APIBoilerplate.Authentication.Commands;
using Net7APIBoilerplate.Authentication.Commands.Handlers;
using Net7APIBoilerplate.Authentication.Data;
using Net7APIBoilerplate.Authentication.Helpers;
using Net7APIBoilerplate.ConfigurationModels;
using Net7APIBoilerPlate.Tests.Plumbing.UnitTesting;
using NSubstitute;
using System.Security.Authentication;
using Xunit;

namespace Net7APIBoilerPlate.Tests.Authentication.Commands.Handlers;

public class LoginCommandHandlerTests : UnitTestFixture<LoginCommandHandler>
{
    private readonly IUserManager _userManager;
    private JwtConfig _jwtConfig;
    private readonly LoginCommand _command;
    private readonly ApplicationUser _user;

    public LoginCommandHandlerTests()
    {
        _userManager = Dependency<IUserManager>();

        _command = new LoginCommand
        {
            Username = "User1",
            Password = "Password123!"
        };

        _user = new ApplicationUser
        {
            UserName = "User1"
        };
    }

    protected override void PreUnderTestResolve(IServiceCollection services)
    {
        _jwtConfig = new JwtConfig();
        services.TryAddScoped(_ => _jwtConfig);
    }

    [Fact]
    public void ShouldThrowExceptionIfUserNotFound()
    {
        _userManager
            .FindByNameAsync("User1")
            .Returns(default(ApplicationUser));
        Assert.ThrowsAsync<InvalidCredentialException>(async () => await UnderTest.Handle(_command));
    }
        
    [Fact]
    public void ShouldThrowExceptionIfInvalidPassword()
    {
        _userManager
            .FindByNameAsync("User1")
            .Returns(_user);

        _userManager
            .CheckPasswordAsync(_user, "Password123!")
            .Returns(false);
            
        Assert.ThrowsAsync<InvalidCredentialException>(async () => await UnderTest.Handle(_command));
    }
}