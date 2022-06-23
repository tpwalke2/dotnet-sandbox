using System.ComponentModel.DataAnnotations;
using Net6APIBoilerplate.Plumbing.Commands;
using Net6APIBoilerplate.Plumbing.Models;

namespace Net6APIBoilerplate.Authentication.Commands;

public sealed record LoginCommand : ICommand<Outcome<LoginResponse>>
{  
    [Required(ErrorMessage = "User Name is required")]  
    public string Username { get; init; }  
  
    [Required(ErrorMessage = "Password is required")]  
    public string Password { get; init; }  
}