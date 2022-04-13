using System;
using System.Collections.Generic;
using System.Linq;

namespace Anvil.Utilities
{
    internal static class TypeCache
    {
        private static readonly Dictionary<Type, string> _names = new Dictionary<Type, string>();
        private static readonly Dictionary<string, Type> _types = new Dictionary<string, Type>();

        public static string GetName(Type type)
        {
            if (_names.TryGetValue(type, out var name))
            {
                return name;
            }

            return _names[type] = GenerateSlimAssemblyName(type);
        }

        public static Type GetType(string name)
        {
            if (_types.TryGetValue(name, out var type))
            {
                return type;
            }

            return _types[name] = Type.GetType(name);
        }

        private static string GenerateSlimAssemblyName(Type type) // Returns Assembly Qualified Name without Version, Culture and PublicKeyToken values
        {
            string Format(string typeNamespace, string typeName, string moduleName)
            {
                return $"{typeNamespace}.{typeName},{moduleName}";
            }

            if (type.IsGenericType)
            {
                var arguments = string.Join("],[", type.GenericTypeArguments.Select(GetName));
                return Format(type.Namespace, $"{type.Name}[[{arguments}]]", type.Assembly.GetName().Name);
            }

            return Format(type.Namespace, type.Name, type.Assembly.GetName().Name);
        }
    }
}