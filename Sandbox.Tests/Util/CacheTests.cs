using Sandbox.Util;
using Xunit;

namespace Sandbox.Tests.Util;

public class CacheTests
{
    [Fact]
    public void CacheMissGeneratesNewItem()
    {
        var underTest = new Cache<string, CacheTestType>(_ => new CacheTestType());

        var result = underTest.Get("input");
        Assert.NotNull(result);
    }

    [Fact]
    public void CacheHitDoesNotGenerateNewItem()
    {
        var cacheValueGeneratorCalled = 0;

        var underTest = new Cache<string, CacheTestType>(_ =>
        {
            cacheValueGeneratorCalled++;
            return new CacheTestType();
        });

        var result1 = underTest.Get("input");
        var result2 = underTest.Get("input");
        Assert.NotNull(result1);
        Assert.Same(result2, result1);
        Assert.Equal(1, cacheValueGeneratorCalled);
    }

    [Fact]
    public void DifferentKeyGeneratesSeparateItem()
    {
        var cacheValueGeneratorCalled = 0;

        var underTest = new Cache<string, CacheTestType>(_ =>
        {
            cacheValueGeneratorCalled++;
            return new CacheTestType();
        });

        var result1 = underTest.Get("input1");
        var result2 = underTest.Get("input2");
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotSame(result2, result1);
        Assert.Equal(2, cacheValueGeneratorCalled);
    }

    [Fact]
    public void CacheInvalidateShouldAllowGeneratingNewItem()
    {
        var cacheValueGeneratorCalled = 0;

        var underTest = new Cache<string, CacheTestType>(_ =>
        {
            cacheValueGeneratorCalled++;
            return new CacheTestType();
        });

        var result1 = underTest.Get("input");
        underTest.Invalidate();
        var result2 = underTest.Get("input");
        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotSame(result2, result1);
        Assert.Equal(2, cacheValueGeneratorCalled);
    }

    [Fact]
    public void InvalidateKeyDoesNotInvalidateAllKeys()
    {
        var underTest = new Cache<string, CacheTestType>(_ => new CacheTestType());

        var result1 = underTest.Get("input1");
        var result2 = underTest.Get("input2");
        underTest.Invalidate("input1");
        var result3 = underTest.Get("input1");
        var result4 = underTest.Get("input2");

        Assert.NotNull(result1);
        Assert.NotNull(result2);
        Assert.NotNull(result3);
        Assert.NotNull(result4);
        Assert.NotSame(result3, result1);
        Assert.Same(result4, result2);
    }

    [Fact]
    public void InvalidateInvalidKey()
    {
        var underTest = new Cache<string, CacheTestType>(_ => new CacheTestType());
        var exception = Record.Exception(() => underTest.Invalidate("input"));
        Assert.Null(exception);
    }

    [Fact]
    public void GeneratedNullShouldNotBeAddedToCache()
    {
        var cacheValueGeneratorCalled = 0;

        var underTest = new Cache<string, CacheTestType>(_ =>
        {
            cacheValueGeneratorCalled++;
            return cacheValueGeneratorCalled == 1 ? null : new CacheTestType();
        });

        var result1 = underTest.Get("input");
        var result2 = underTest.Get("input");
        Assert.Null(result1);
        Assert.NotNull(result2);
        Assert.Equal(2, cacheValueGeneratorCalled);
    }

    public class CacheTestType
    {
    }
}