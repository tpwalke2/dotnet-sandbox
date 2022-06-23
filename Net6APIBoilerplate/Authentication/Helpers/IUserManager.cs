using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Net6APIBoilerplate.Authentication.Data;

namespace Net6APIBoilerplate.Authentication.Helpers;

public interface IUserManager
{
    Task<ApplicationUser> FindByNameAsync(string userName);
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
}