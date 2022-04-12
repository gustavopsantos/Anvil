using System;
using Anvil.Utilities;

namespace Anvil.Serialization
{
    public readonly struct Schema
    {
        public readonly Type Type;

        public Schema(Type type)
        {
            Type = type;
        }

        public void Validate()
        {
            EnsureThat.IsNotNull(Type, "Schema should not be populated with null type");
        }
    }
}