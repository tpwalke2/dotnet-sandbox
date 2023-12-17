using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace APIBoilerplate.Authentication.Data;

public class AuthenticationDbContext : IdentityDbContext<ApplicationUser>
{
    public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
    {
    }
}