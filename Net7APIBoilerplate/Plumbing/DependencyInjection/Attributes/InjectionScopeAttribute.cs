using Microsoft.Extensions.DependencyInjection;
using System;

namespace Net7APIBoilerplate.Plumbing.DependencyInjection.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class InjectionScopeAttribute: Attribute
{
    public InjectionScopeAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
        
    public ServiceLifetime Lifetime { get; }
}