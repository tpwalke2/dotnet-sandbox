using Net7APIBoilerplate.Authentication.Data;
using Net7APIBoilerplate.Authentication.Helpers;
using Net7APIBoilerplate.Plumbing.Commands;
using Net7APIBoilerplate.Plumbing.Models;
using Net7APIBoilerplate.Plumbing.Validation;
using System;
using System.Threading.Tasks;

namespace Net7APIBoilerplate.Authentication.Commands.Handlers;

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