using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Net6APIBoilerplate.ConfigurationModels;
using Net6APIBoilerplate.Plumbing.Helpers;

namespace Net6APIBoilerplate.Plumbing.Extensions.ServiceCollection;

public static class Configurations
{
    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null) throw new ArgumentNullException(nameof(services));
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));

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
            configuration.GetSection(configSectionAttribute.SectionRoot)?
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