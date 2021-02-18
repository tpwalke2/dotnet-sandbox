using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Net5._0APIBoilerplate.Plumbing.Extensions.ServiceCollection
{
    public static class Swagger
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(swaggerGenOptions =>
            {
                swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "ShopShare.Api", Version = "v1" });
                // To Enable authorization using Swagger (JWT)    
                swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {  
                    Name = "Authorization",  
                    Type = SecuritySchemeType.ApiKey,  
                    Scheme = "Bearer",  
                    BearerFormat = "JWT",  
                    In = ParameterLocation.Header,  
                    Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",  
                });  
                swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement  
                {  
                    {  
                        new OpenApiSecurityScheme  
                        {  
                            Reference = new OpenApiReference  
                            {  
                                Type = ReferenceType.SecurityScheme,  
                                Id   = "Bearer"  
                            }  
                        },
                        System.Array.Empty<string>()
                    }  
                });
            });

            return services;
        }
    }
}
