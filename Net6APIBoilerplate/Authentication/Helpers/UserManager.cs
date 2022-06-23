using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Net6APIBoilerplate.Authentication.Data;

namespace Net6APIBoilerplate.Authentication.Helpers;

public class UserManager: IUserManager
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManager(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
        
    public async Task<ApplicationUser> FindByNameAsync(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }
}