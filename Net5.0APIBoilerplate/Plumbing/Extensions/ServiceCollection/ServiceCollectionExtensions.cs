using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Net5._0APIBoilerplate.Plumbing.DependencyInjection.Attributes;

namespace Net5._0APIBoilerplate.Plumbing.Extensions.ServiceCollection
{
    public static class ServiceCollectionExtensions
    {
        private static ServiceLifetime GetLifetime(ICustomAttributeProvider t)
        {
            var tAttr = t.GetCustomAttributes(typeof(InjectionScopeAttribute), false).FirstOrDefault();
            return ((InjectionScopeAttribute) tAttr)?.Lifetime ?? ServiceLifetime.Scoped;
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
}
