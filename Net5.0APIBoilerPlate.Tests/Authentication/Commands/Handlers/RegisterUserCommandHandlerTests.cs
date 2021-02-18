using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Moq;
using Net5._0APIBoilerplate.Authentication.Commands;
using Net5._0APIBoilerplate.Authentication.Commands.Handlers;
using Net5._0APIBoilerplate.Authentication.Data;
using Net5._0APIBoilerplate.Authentication.Helpers;
using Net5._0APIBoilerplate.Plumbing.Validation;
using Net5._0APIBoilerPlate.Tests.Plumbing.UnitTesting;
using NUnit.Framework;

namespace Net5._0APIBoilerPlate.Tests.Authentication.Commands.Handlers
{
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
}
