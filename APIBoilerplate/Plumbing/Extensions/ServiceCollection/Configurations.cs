using APIBoilerplate.ConfigurationModels;
using APIBoilerplate.Plumbing.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace APIBoilerplate.Plumbing.Extensions.ServiceCollection;

public static class Configurations
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        TypeHelpers.GetTypesWithAttribute<ConfigSectionAttribute>()
                   .ForEach(CreateRegisterMethod(services, configuration));

        return services;
    }

    private static Action<(ConfigSectionAttribute, Type)> CreateRegisterMethod(
        IServiceCollection services,
        IConfiguration configuration)
    {
        return at =>
        {
            var (configSectionAttribute, configType) = at;
            configuration.GetSection(configSectionAttribute.SectionRoot)
                .Pipe(configSection =>
                {
                    if (configSection == null) return;
                    Activator.CreateInstance(configType)
                             .Pipe(model =>
                             {
                                 configSection.Bind(model);
                                 services.AddSingleton(configType, model);
                             });
                });
        };
    }
}