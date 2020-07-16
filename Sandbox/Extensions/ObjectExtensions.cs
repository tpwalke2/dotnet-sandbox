using System;
using System.Collections.Generic;

namespace Sandbox.Extensions
{
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

        public static bool IsDefault<T>(this T value)
        {
            return EqualityComparer<T>.Default.Equals(value, default);
        }
    }
}
