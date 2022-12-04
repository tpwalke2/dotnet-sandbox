using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net7APIBoilerplate.Authentication.Data;

namespace Net7APIBoilerplate.Plumbing.Extensions.ServiceCollection;

public static class DbContexts
{
    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AuthenticationDbContext>(
            options => options.UseSqlServer(configuration["ConnectionStrings:PrimaryConnection"]));

        return services;
    }    
}