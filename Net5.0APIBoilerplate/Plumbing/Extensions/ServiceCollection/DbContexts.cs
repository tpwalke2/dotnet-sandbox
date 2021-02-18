using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net5._0APIBoilerplate.Authentication.Data;

namespace Net5._0APIBoilerplate.Plumbing.Extensions.ServiceCollection
{
    public static class DbContexts
    {
        public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthenticationDbContext>(
                options => options.UseSqlServer(configuration["ConnectionStrings:PrimaryConnection"]));

            return services;
        }    
    }
}
