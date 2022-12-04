using Microsoft.IdentityModel.Tokens;
using Net7APIBoilerplate.Authentication.Data;
using Net7APIBoilerplate.Authentication.Helpers;
using Net7APIBoilerplate.ConfigurationModels;
using Net7APIBoilerplate.Plumbing.Commands;
using Net7APIBoilerplate.Plumbing.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Net7APIBoilerplate.Authentication.Commands.Handlers;

public class LoginCommandHandler: ICommandHandler<LoginCommand, LoginResponse>
{
    private readonly IUserManager _userManager;
    private readonly JwtConfig _jwtConfig;

    public LoginCommandHandler(IUserManager userManager, JwtConfig jwtConfig)
    {
        _userManager = userManager;
        _jwtConfig   = jwtConfig;
    }
        
    public async Task<Outcome<LoginResponse>> Handle(LoginCommand command)
    {
        var user = await _userManager.FindByNameAsync(command.Username);
        await EnsureValidCredentials(user, command.Password);

        var userRoles      = await _userManager.GetRolesAsync(user);
        var authClaims     = GetAuthClaims(user, userRoles);
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
        var token          = CreateSecurityToken(authClaims, authSigningKey);

        return new Outcome<LoginResponse>
        {
            Result = new LoginResponse
            {
                Token      = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            },
            Success = true
        };
    }

    private JwtSecurityToken CreateSecurityToken(IEnumerable<Claim> authClaims, SecurityKey authSigningKey)
    {
        return new JwtSecurityToken(
            issuer: _jwtConfig.ValidIssuer,
            audience: _jwtConfig.ValidAudience,
            expires: DateTime.Now.AddHours(3),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
    }

    private static IEnumerable<Claim> GetAuthClaims(ApplicationUser user, IEnumerable<string> userRoles)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));
        return authClaims;
    }

    private async Task EnsureValidCredentials(ApplicationUser user, string password)
    {
        if (user == null || !await _userManager.CheckPasswordAsync(user, password))
            throw new InvalidCredentialException();
    }
}