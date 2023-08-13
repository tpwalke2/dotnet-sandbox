using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Sandbox.Extensions;

namespace Sandbox.Tests.Extensions;

[TestFixture]
public class EnumerableExtensionsTests
{
    [Test]
    public void Combinate_NullOther()
    {
        var           list1 = new[] {1, 2};
        IList<string> list2 = null;

        var result = list1.Combinate(list2);
            
        Assert.That(result, Is.Empty);
    }
        
    [Test]
    public void Combinate_NullSource()
    {
        var           list1 = new[] {1, 2};
        IList<string> list2 = null;

        var result = list2.Combinate(list1);
            
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Combinate_EmptySets()
    {
        var list1 = System.Array.Empty<int>();
        var list2 = System.Array.Empty<string>();

        var result = list1.Combinate(list2).ToList();
            
        Assert.That(result.Count, Is.EqualTo(0));
    }
        
    [Test]
    public void Combinate_EmptyOther()
    {
        var list1 = new[] {1, 2};
        var list2 = System.Array.Empty<string>();

        var result = list1.Combinate(list2);
            
        Assert.That(result.Count, Is.EqualTo(0));
    }
        
    [Test]
    public void Combinate()
    {
        var list1 = new[] {1, 2};
        var list2 = new[] {"a"};

        var result = list1.Combinate(list2).ToList();
            
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That(result[0].Item1, Is.EqualTo(1));
        Assert.That(result[0].Item2, Is.EqualTo("a"));
        Assert.That(result[1].Item1, Is.EqualTo(2));
        Assert.That(result[1].Item2, Is.EqualTo("a"));
    }
}