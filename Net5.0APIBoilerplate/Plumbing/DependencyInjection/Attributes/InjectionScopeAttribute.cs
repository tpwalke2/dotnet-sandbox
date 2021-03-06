using System;
using Microsoft.Extensions.DependencyInjection;

namespace Net5._0APIBoilerplate.Plumbing.DependencyInjection.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class InjectionScopeAttribute: Attribute
    {
        public InjectionScopeAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            Lifetime = lifetime;
        }
        
        public ServiceLifetime Lifetime { get; }
    }
}
