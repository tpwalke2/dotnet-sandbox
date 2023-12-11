using System;

namespace APIBoilerplate.Plumbing.Extensions;

public static class ObjectExtensions
{
    public static TResult Pipe<T, TResult>(this T value, Func<T, TResult> pipeFunction)
    {
        return pipeFunction(value);
    }
        
    public static void Pipe<T>(this T value, Action<T> pipeFunction)
    {
        pipeFunction(value);
    }
}