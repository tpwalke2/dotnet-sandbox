using APIBoilerplate.Authentication.Commands;
using APIBoilerplate.Authentication.Commands.Handlers;
using APIBoilerplate.Authentication.Data;
using APIBoilerplate.Authentication.Helpers;
using APIBoilerplate.ConfigurationModels;
using APIBoilerPlate.Tests.Plumbing.UnitTesting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NSubstitute;
using System.Security.Authentication;
using System.Threading.Tasks;
using Xunit;

namespace APIBoilerPlate.Tests.Authentication.Commands.Handlers;

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
    public async Task ShouldThrowExceptionIfUserNotFound()
    {
        _userManager
            .FindByNameAsync("User1")
            .Returns(default(ApplicationUser));
        await Assert.ThrowsAsync<InvalidCredentialException>(() => UnderTest.Handle(_command));
    }
        
    [Fact]
    public async Task ShouldThrowExceptionIfInvalidPassword()
    {
        _userManager
            .FindByNameAsync("User1")
            .Returns(_user);

        _userManager
            .CheckPasswordAsync(_user, "Password123!")
            .Returns(false);
            
        await Assert.ThrowsAsync<InvalidCredentialException>(() => UnderTest.Handle(_command));
    }
}