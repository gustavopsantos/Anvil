using System;
using System.Linq;

namespace Anvil.Extensions
{
    internal static class TypeExtensions
    {
        public static string GetFormattedName(this Type type)
        {
            if (type.IsGenericType)
            {
                var genericArguments = string.Join(", ", type.GetGenericArguments().Select(GetFormattedName));
                var typeName = type.Name.Substring(0, type.Name.IndexOf('`'));
                return $"{typeName}<{genericArguments}>";
            }

            return type.Name;
        }
    }
}