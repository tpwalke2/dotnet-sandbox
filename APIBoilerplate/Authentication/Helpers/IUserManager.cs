using APIBoilerplate.Authentication.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIBoilerplate.Authentication.Helpers;

public interface IUserManager
{
    Task<ApplicationUser> FindByNameAsync(string userName);
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
}