using APIBoilerplate.Plumbing.Commands;
using APIBoilerplate.Plumbing.Models;
using System.ComponentModel.DataAnnotations;

namespace APIBoilerplate.Authentication.Commands;

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