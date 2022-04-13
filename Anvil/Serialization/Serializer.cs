using System;
using Anvil.Abstractions;
using Anvil.Extensions;
using Anvil.Serializers;
using Anvil.Utilities;

namespace Anvil.Serialization
{
    public class Serializer
    {
        private readonly byte[] _buffer;
        public readonly SerializationModel SerializationModel;
        private readonly AGenericSerializer<Schema> _schemaSerializer;

        public Serializer(Logger logger)
        {
            _buffer = new byte[short.MaxValue];
            SerializationModel = new SerializationModel(logger);
            SerializationModel.Add<int>(new IntSerializer());
            SerializationModel.Add<byte>(new ByteSerializer());
            SerializationModel.Add<string>(new StringSerializer(SerializationModel));
            SerializationModel.Add<Schema>(new SchemaSerializer(SerializationModel));
            _schemaSerializer = SerializationModel.Get<Schema>();
        }
        
        public byte[] Serialize<T>(T obj)
        {
            if (obj == null)
            {
                return Array.Empty<byte>();
            }

            var offset = 0;
            _schemaSerializer.Serialize(new Schema(typeof(T)), _buffer, ref offset);
            SerializationModel.Get<T>().Serialize(obj, _buffer, ref offset);
            return _buffer.Copy(offset);
        }

        public object Deserialize(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            var offset = 0;
            var schema = _schemaSerializer.Deserialize(bytes, ref offset);
            return SerializationModel.Get(schema.Type).DeserializeObject(bytes, ref offset);
        }
    }
}