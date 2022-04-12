using System;
using System.Diagnostics;

namespace Anvil.Utilities
{
    internal static class EnsureThat
    {
        [Conditional("DEBUG")]
        public static void IsNotNull<T>(T obj, string message) where T : class
        {
            if (obj == null)
            {
                throw new Exception(message);
            }
        }
    }
}