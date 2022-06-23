using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Net6APIBoilerplate.Authentication.Commands;
using Net6APIBoilerplate.Plumbing.Commands;
using Net6APIBoilerplate.Plumbing.Validation;

namespace Net6APIBoilerplate.Authentication;

[Route("api/[controller]")]
[ApiController]  
public class AuthenticateController : ControllerBase  
{
    private readonly ICommandHandler<LoginCommand, LoginResponse> _loginHandler;
    private readonly ICommandHandler<RegisterUserCommand> _registerHandler;
  
    public AuthenticateController(ICommandHandler<LoginCommand, LoginResponse> loginHandler,
                                  ICommandHandler<RegisterUserCommand> registerHandler)  
    {
        _loginHandler    = loginHandler;
        _registerHandler = registerHandler;
    }

    [HttpPost]  
    [Route("login")]  
    public async Task<IActionResult> Login([FromBody] LoginCommand command)  
    {
        try
        {
            var result = await _loginHandler.Handle(command);
            return result.Success 
                ? Ok(result.Result)
                : Unauthorized();
        }
        catch (InvalidCredentialsException)
        {
            return Unauthorized();
        }
    }  
  
    [HttpPost]  
    [Route("register")]  
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand userCommand)  
    {
        try
        {
            var result = await _registerHandler.Handle(userCommand);
            return result.Success
                ? Ok("User created successfully!")
                : StatusCode(StatusCodes.Status500InternalServerError);
        }
        catch (InvalidArgumentsException ex)
        {
            return UnprocessableEntity(ex.Message);
        }
    }
}