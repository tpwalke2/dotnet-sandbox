using APIBoilerplate.Plumbing.Commands;
using APIBoilerplate.Plumbing.Models;
using System.ComponentModel.DataAnnotations;

namespace APIBoilerplate.Authentication.Commands;

public sealed record LoginCommand : ICommand<Outcome<LoginResponse>>
{  
    [Required(ErrorMessage = "User Name is required")]  
    public string Username { get; init; }  
  
    [Required(ErrorMessage = "Password is required")]  
    public string Password { get; init; }  
}