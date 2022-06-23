using System.ComponentModel.DataAnnotations;
using Net6APIBoilerplate.Plumbing.Commands;
using Net6APIBoilerplate.Plumbing.Models;

namespace Net6APIBoilerplate.Authentication.Commands;

public record RegisterUserCommand : ICommand<Outcome>
{  
    [Required(ErrorMessage = "User Name is required")]  
    public string Username { get; init; }  
  
    [EmailAddress]  
    [Required(ErrorMessage = "Email is required")]  
    public string Email { get; init; }  
  
    [Required(ErrorMessage = "Password is required")]  
    public string Password { get; init; }  
}