using System.Collections.Generic;
using System.Linq;
using Sandbox.Extensions;
using Xunit;

namespace Sandbox.Tests.Extensions;

public class EnumerableExtensionsTests
{
    [Fact]
    public void Combinate_NullOther()
    {
        var           list1 = new[] { 1, 2 };
        IList<string>? list2 = null;

        var result = list1.Combinate(list2);

        Assert.Empty(result);
    }

    [Fact]
    public void Combinate_NullSource()
    {
        var           list1 = new[] { 1, 2 };
        IList<string>? list2 = null;

        var result = list2.Combinate(list1);

        Assert.Empty(result);
    }

    [Fact]
    public void Combinate_EmptySets()
    {
        var list1 = System.Array.Empty<int>();
        var list2 = System.Array.Empty<string>();

        var result = list1.Combinate(list2).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void Combinate_EmptyOther()
    {
        var list1 = new[] { 1, 2 };
        var list2 = System.Array.Empty<string>();

        var result = list1.Combinate(list2);

        Assert.Empty(result);
    }

    [Fact]
    public void Combinate()
    {
        var list1 = new[] { 1, 2 };
        var list2 = new[] { "a" };

        var result = list1.Combinate(list2).ToList();

        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Item1);
        Assert.Equal("a", result[0].Item2);
        Assert.Equal(2, result[1].Item1);
        Assert.Equal("a", result[1].Item2);
    }
}