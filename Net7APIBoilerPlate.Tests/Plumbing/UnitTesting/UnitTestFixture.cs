using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Net7APIBoilerplate.Plumbing.Extensions;
using Net7APIBoilerplate.Plumbing.Helpers;
using NUnit.Framework;

namespace Net7APIBoilerPlate.Tests.Plumbing.UnitTesting;

[TestFixture]
public abstract class UnitTestFixture<TUnderTest> where TUnderTest : class
{
    public TUnderTest UnderTest;

    protected ServiceProvider Provider;

    [SetUp]
    public void BaseSetUp()
    {
        var services = new ServiceCollection();
        AddMockedDependencies(services);
        PreUnderTestResolve(services);
        Provider  = services.BuildServiceProvider();
        UnderTest = Provider.GetRequiredService<TUnderTest>();
    }

    [TearDown]
    public void BaseTearDown()
    {
        Provider.Dispose();
    }

    protected Mock<TDependency> Dependency<TDependency>() where TDependency : class
    {
        return Provider.GetService<Mock<TDependency>>();
    }

    private static void AddMockedDependencies(IServiceCollection services)
    {
        TypeHelpers.GetConstructorParameterTypes<TUnderTest>()
                   .ForEach(t =>
                   {
                       var mockedType = typeof(Mock<>).MakeGenericType(t);
                       services.TryAddScoped(mockedType);
                       services.TryAddScoped(t, provider => ((Mock) provider.GetService(mockedType))?.Object);
                   });

        services.TryAddScoped(typeof(TUnderTest));
    }

    protected virtual void PreUnderTestResolve(IServiceCollection services)
    {
        // Provided for child classes to implement
    }
}