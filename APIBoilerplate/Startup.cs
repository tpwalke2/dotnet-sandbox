using APIBoilerplate.Plumbing.Extensions.ServiceCollection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace APIBoilerplate;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
            
        services.AddConfigurations(Configuration)
                .AddDbContexts(Configuration)
                .AddAuthentication(Configuration)
                .AddSwagger()
                .AddCommandHandlers()
                .AddQueryHandlers();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage()
               .UseSwagger()
               .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", ".NET 7 API v1"));
        }

        app.UseHttpsRedirection()
           .UseRouting()
           .UseAuthentication()
           .UseAuthorization()
           .UseEndpoints(endpoints =>
           {
               endpoints.MapControllers();
           });
    }
}