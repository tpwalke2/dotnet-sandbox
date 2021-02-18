using System;
using System.Threading.Tasks;
using Net5._0APIBoilerplate.Authentication.Data;
using Net5._0APIBoilerplate.Authentication.Helpers;
using Net5._0APIBoilerplate.Plumbing.Commands;
using Net5._0APIBoilerplate.Plumbing.Validation;

namespace Net5._0APIBoilerplate.Authentication.Commands.Handlers
{
    public class RegisterUserCommandHandler: ICommandHandler<RegisterUserCommand>
    {
        private readonly IUserManager _userManager;

        public RegisterUserCommandHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }
        
        public async Task<Outcome> Handle(RegisterUserCommand command)
        {
            await EnsureValidParameters(command.Username);
            
            var user = new ApplicationUser
            {  
                Email         = command.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),  
                UserName      = command.Username  
            };
            
            var result = await _userManager.CreateAsync(user, command.Password);

            return result.Succeeded
                ? new Outcome {Success = true}
                : new Outcome
                {
                    Success      = false,
                    ErrorMessage = "User creation failed! Please check user details and try again."
                };
        }

        private async Task EnsureValidParameters(string userName)
        {
            var userExists = await _userManager.FindByNameAsync(userName);
            if (userExists != null) throw new InvalidArgumentsException("User already exists!");  
        }
    }
}
