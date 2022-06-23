using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net6APIBoilerplate.Authentication.Data;

namespace Net6APIBoilerplate.Plumbing.Extensions.ServiceCollection;

public static class DbContexts
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthenticationDbContext>(
            options => options.UseSqlServer(configuration["ConnectionStrings:PrimaryConnection"]));

        return services;
    }    
}