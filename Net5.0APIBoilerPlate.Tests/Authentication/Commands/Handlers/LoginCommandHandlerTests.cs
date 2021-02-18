using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Net5._0APIBoilerplate.Authentication.Commands;
using Net5._0APIBoilerplate.Authentication.Commands.Handlers;
using Net5._0APIBoilerplate.Authentication.Data;
using Net5._0APIBoilerplate.Authentication.Helpers;
using Net5._0APIBoilerplate.ConfigurationModels;
using Net5._0APIBoilerPlate.Tests.Plumbing.UnitTesting;
using NUnit.Framework;

namespace Net5._0APIBoilerPlate.Tests.Authentication.Commands.Handlers
{
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
            Assert.ThrowsAsync<InvalidCredentialsException>(async () => await UnderTest.Handle(_command));
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
            
            Assert.ThrowsAsync<InvalidCredentialsException>(async () => await UnderTest.Handle(_command));
        }
    }
}
