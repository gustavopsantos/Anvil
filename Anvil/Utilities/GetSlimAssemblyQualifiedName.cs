using System;
using System.Collections.Generic;
using System.Linq;

namespace Anvil.Utilities
{
    // Returns Assembly Qualified Name without Version, Culture and PublicKeyToken values
    public static class GetSlimAssemblyQualifiedName
    {
        private static readonly Dictionary<Type, string> _registry = new();

        public static string Get(Type type)
        {
            if (_registry.TryGetValue(type, out var value))
            {
                return value;
            }

            return _registry[type] = Generate(type);
        }

        private static string Generate(Type type)
        {
            string Format(string typeNamespace, string typeName, string moduleName)
            {
                return $"{typeNamespace}.{typeName},{moduleName}";
            }

            if (type.IsGenericType)
            {
                var arguments = string.Join("],[", type.GenericTypeArguments.Select(Get));
                return Format(type.Namespace, $"{type.Name}[[{arguments}]]", type.Assembly.GetName().Name);
            }

            return Format(type.Namespace, type.Name, type.Assembly.GetName().Name);
        }
    }
}