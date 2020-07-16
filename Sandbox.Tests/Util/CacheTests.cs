using NUnit.Framework;
using Sandbox.Util;

namespace Sandbox.Tests.Util
{
    [TestFixture]
    public class CacheTests
    {
        [Test]
        public void CacheMissGeneratesNewItem()
        {
            var underTest = new Cache<string, CacheTestType>(s =>
            {
                return new CacheTestType();
            });

            var result = underTest.Get("input");
            Assert.That(result, Is.Not.Null);
        }
        
        [Test]
        public void CacheHitDoesNotGenerateNewItem()
        {
            var cacheValueGeneratorCalled = 0;

            var underTest = new Cache<string, CacheTestType>(s =>
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
        public void GeneratedNullShouldNotBeAddedToCache()
        {
            var cacheValueGeneratorCalled = 0;

            var underTest = new Cache<string, CacheTestType>(s =>
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
}
