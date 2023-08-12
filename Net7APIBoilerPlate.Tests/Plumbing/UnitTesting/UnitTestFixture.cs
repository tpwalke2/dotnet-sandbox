using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Net7APIBoilerplate.Plumbing.Extensions;
using Net7APIBoilerplate.Plumbing.Helpers;
using NSubstitute;
using System;

namespace Net7APIBoilerPlate.Tests.Plumbing.UnitTesting;

public abstract class UnitTestFixture<TUnderTest> : IDisposable where TUnderTest : class
{
    protected readonly TUnderTest UnderTest;

    private readonly ServiceProvider _provider;

    protected UnitTestFixture()
    {
        var services = new ServiceCollection();
        AddMockedDependencies(services);
        PreUnderTestResolve(services);
        _provider  = services.BuildServiceProvider();
        UnderTest = _provider.GetRequiredService<TUnderTest>();
    }

    protected TDependency Dependency<TDependency>() where TDependency : class
    {
        return _provider.GetService<TDependency>();
    }

    private static void AddMockedDependencies(IServiceCollection services)
    {
        TypeHelpers.GetConstructorParameterTypes<TUnderTest>()
                   .ForEach(t =>
                   {
                       var mockedType = Substitute.For(new[] { t }, Array.Empty<object>());
                       services.TryAddScoped(t, _ => mockedType);
                   });

        services.TryAddScoped(typeof(TUnderTest));
    }

    protected virtual void PreUnderTestResolve(IServiceCollection services)
    {
        // Provided for child classes to implement
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        _provider?.Dispose();
    }
}