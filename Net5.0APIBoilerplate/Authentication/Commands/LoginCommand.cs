using System.ComponentModel.DataAnnotations;
using Net5._0APIBoilerplate.Plumbing.Commands;

namespace Net5._0APIBoilerplate.Authentication.Commands
{
    public sealed record LoginCommand : ICommand<Outcome<LoginResponse>>
    {  
        [Required(ErrorMessage = "User Name is required")]  
        public string Username { get; init; }  
  
        [Required(ErrorMessage = "Password is required")]  
        public string Password { get; init; }  
    }
}
