using APIBoilerplate.Authentication.Data;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIBoilerplate.Authentication.Helpers;

public class UserManager: IUserManager
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserManager(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
        
    public Task<ApplicationUser> FindByNameAsync(string userName)
    {
        return _userManager.FindByNameAsync(userName);
    }

    public Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return _userManager.GetRolesAsync(user);
    }

    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
    {
        return _userManager.CheckPasswordAsync(user, password);
    }

    public Task<IdentityResult> CreateAsync(ApplicationUser user, string password)
    {
        return _userManager.CreateAsync(user, password);
    }
}