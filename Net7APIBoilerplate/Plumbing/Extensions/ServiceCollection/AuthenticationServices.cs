using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Net7APIBoilerplate.Authentication.Data;
using System.Text;

namespace Net7APIBoilerplate.Plumbing.Extensions.ServiceCollection;

public static class AuthenticationServices
{
    public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        // For Identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();
  
        // Adding Authentication  
        services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme             = JwtBearerDefaults.AuthenticationScheme;
                })
                // Adding Jwt Bearer  
                .AddJwtBearer(options =>
                {  
                    options.SaveToken            = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {  
                        ValidateIssuer   = true,
                        ValidateAudience = true,
                        ValidAudience    = configuration["JWT:ValidAudience"],
                        ValidIssuer      = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
                    };
                });

        return services;
    } 
}