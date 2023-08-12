using NUnit.Framework;
using Sandbox.Util;

namespace Sandbox.Tests.Util;

[TestFixture]
public class CacheTests
{
    [Test]
    public void CacheMissGeneratesNewItem()
    {
        var underTest = new Cache<string, CacheTestType>(_ => new CacheTestType());

        var result = underTest.Get("input");
        Assert.That(result, Is.Not.Null);
    }
        
    [Test]
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
        Assert.That(result1, Is.Not.Null);
        Assert.That(result1, Is.SameAs(result2));
        Assert.That(cacheValueGeneratorCalled, Is.EqualTo(1));
    }
        
    [Test]
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
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(result1, Is.Not.SameAs(result2));
        Assert.That(cacheValueGeneratorCalled, Is.EqualTo(2));
    }

    [Test]
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
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(result1, Is.Not.SameAs(result2));
        Assert.That(cacheValueGeneratorCalled, Is.EqualTo(2));
    }
        
    [Test]
    public void InvalidateKeyDoesNotInvalidateAllKeys()
    {
        var underTest = new Cache<string, CacheTestType>(_ => new CacheTestType());

        var result1 = underTest.Get("input1");
        var result2 = underTest.Get("input2");
        underTest.Invalidate("input1");
        var result3 = underTest.Get("input1");
        var result4 = underTest.Get("input2");
            
        Assert.That(result1, Is.Not.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(result3, Is.Not.Null);
        Assert.That(result4, Is.Not.Null);
        Assert.That(result1, Is.Not.SameAs(result3));
        Assert.That(result2, Is.SameAs(result4));
    }

    [Test]
    public void InvalidateInvalidKey()
    {
        var underTest = new Cache<string, CacheTestType>(_ => new CacheTestType());
        Assert.That(() => underTest.Invalidate("input"), Throws.Nothing);
    }

    [Test]
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
        Assert.That(result1, Is.Null);
        Assert.That(result2, Is.Not.Null);
        Assert.That(cacheValueGeneratorCalled, Is.EqualTo(2));
    }
        
    public class CacheTestType { }
}