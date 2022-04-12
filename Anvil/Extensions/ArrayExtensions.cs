using System;

namespace Anvil.Extensions
{
    internal static class ArrayExtensions
    {
        public static T[] Copy<T>(this T[] source, int length)
        {
            var copy = new T[length];
            Array.Copy(source, 0, copy, 0, length);
            return copy;
        }
    }
}