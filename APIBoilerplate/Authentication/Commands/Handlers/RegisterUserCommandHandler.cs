using APIBoilerplate.Authentication.Data;
using APIBoilerplate.Authentication.Helpers;
using APIBoilerplate.Plumbing.Commands;
using APIBoilerplate.Plumbing.Models;
using APIBoilerplate.Plumbing.Validation;
using System;
using System.Threading.Tasks;

namespace APIBoilerplate.Authentication.Commands.Handlers;

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