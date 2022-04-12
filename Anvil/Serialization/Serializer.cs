using System;
using Anvil.Extensions;
using Anvil.Serializers;
using Anvil.Utilities;

namespace Anvil.Serialization
{
    public class Serializer
    {
        private readonly byte[] _buffer;
        private readonly SerializationModel _serializationModel;

        public Serializer(Logger logger)
        {
            _buffer = new byte[short.MaxValue];
            _serializationModel = new SerializationModel(logger);
            _serializationModel.Add<int>(new IntSerializer());
            _serializationModel.Add<byte>(new ByteSerializer());
            _serializationModel.Add<string>(new StringSerializer(_serializationModel));
            _serializationModel.Add<Schema>(new SchemaSerializer(_serializationModel));
        }
        
        public byte[] Serialize<T>(T obj)
        {
            if (obj == null)
            {
                return Array.Empty<byte>();
            }

            var offset = 0;
            _serializationModel.Get<Schema>().Serialize(new Schema(typeof(T)), _buffer, ref offset);
            _serializationModel.Get<T>().Serialize(obj, _buffer, ref offset);
            return _buffer.Copy(offset);
        }

        public object Deserialize(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                return null;
            }

            var offset = 0;
            var schema = _serializationModel.Get<Schema>().Deserialize(bytes, ref offset);
            return _serializationModel.Get(schema.Type).DeserializeObject(bytes, ref offset);
        }
    }
}