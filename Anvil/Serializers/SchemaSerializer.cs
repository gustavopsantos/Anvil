using System;
using Anvil.Abstractions;
using Anvil.Serialization;
using Anvil.Utilities;

namespace Anvil.Serializers
{
    public class SchemaSerializer : AGenericSerializer<Schema>
    {
        private readonly AGenericSerializer<string> _stringSerializer;

        public SchemaSerializer(SerializationModel serializationModel)
        {
            _stringSerializer = serializationModel.Get<string>();
        }

        public override void Serialize(Schema value, byte[] bytes, ref int offset)
        {
            //value.Validate();
            var assemblyQualifiedName = TypeCache.GetName(value.Type);
            _stringSerializer.Serialize(assemblyQualifiedName, bytes, ref offset);
        }

        public override Schema Deserialize(byte[] bytes, ref int offset)
        {
            var assemblyQualifiedName = _stringSerializer.Deserialize(bytes, ref offset);
            var type = TypeCache.GetType(assemblyQualifiedName);
            var schema = new Schema(type);
            //schema.Validate();
            return schema;
        }
    }
}