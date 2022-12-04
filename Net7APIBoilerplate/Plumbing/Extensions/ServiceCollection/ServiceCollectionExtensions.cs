using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Net7APIBoilerplate.Plumbing.DependencyInjection.Attributes;
using System;
using System.Reflection;

namespace Net7APIBoilerplate.Plumbing.Extensions.ServiceCollection;

public static class ServiceCollectionExtensions
{
    private static ServiceLifetime GetLifetime(ICustomAttributeProvider t)
    {
        return t.GetAttribute<InjectionScopeAttribute>()?.Lifetime ?? ServiceLifetime.Scoped;
    }

    public static void TryAdd(this IServiceCollection services, (Type, Type) t)
    {
        services.TryAdd(ServiceDescriptor
                            .Describe(
                                t.Item1,
                                t.Item2,
                                GetLifetime(t.Item2)));
    }
}