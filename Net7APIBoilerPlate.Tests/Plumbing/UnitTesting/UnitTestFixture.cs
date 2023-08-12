using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Net7APIBoilerplate.Plumbing.Extensions;
using Net7APIBoilerplate.Plumbing.Helpers;
using NSubstitute;
using NUnit.Framework;
using System;

namespace Net7APIBoilerPlate.Tests.Plumbing.UnitTesting;

[TestFixture]
public abstract class UnitTestFixture<TUnderTest> where TUnderTest : class
{
    protected TUnderTest UnderTest;

    private ServiceProvider _provider;

    [SetUp]
    public void BaseSetUp()
    {
        var services = new ServiceCollection();
        AddMockedDependencies(services);
        PreUnderTestResolve(services);
        _provider  = services.BuildServiceProvider();
        UnderTest = _provider.GetRequiredService<TUnderTest>();
    }

    [TearDown]
    public void BaseTearDown()
    {
        _provider.Dispose();
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
}