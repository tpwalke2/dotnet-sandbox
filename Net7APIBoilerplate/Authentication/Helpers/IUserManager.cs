using Microsoft.AspNetCore.Identity;
using Net7APIBoilerplate.Authentication.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Net7APIBoilerplate.Authentication.Helpers;

public interface IUserManager
{
    Task<ApplicationUser> FindByNameAsync(string userName);
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
    Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    Task<IdentityResult> CreateAsync(ApplicationUser user, string password);
}