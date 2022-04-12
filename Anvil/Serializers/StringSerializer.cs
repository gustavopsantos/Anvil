using System.Text;
using Anvil.Abstractions;
using Anvil.Serialization;
using Anvil.Utilities;

namespace Anvil.Serializers
{
    public class StringSerializer : AGenericSerializer<string>
    {
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly AGenericSerializer<byte> _byteSerializer;
        private readonly AGenericSerializer<int> _intSerializer;

        public StringSerializer(SerializationModel serializationModel)
        {
            _byteSerializer = serializationModel.Get<byte>();
            _intSerializer = serializationModel.Get<int>();
        }
        
        public override void Serialize(string value, byte[] bytes, ref int offset)
        {
            if (value == null)
            {
                _byteSerializer.Serialize(NullFlag.Null, bytes, ref offset);
            }
            else
            {
                var byteCount = _encoding.GetByteCount(value);
                _byteSerializer.Serialize(NullFlag.NotNull, bytes, ref offset);
                _intSerializer.Serialize(byteCount, bytes, ref offset);
                _encoding.GetBytes(value, 0, value.Length, bytes, offset);
                offset += byteCount;
            }
        }

        public override string Deserialize(byte[] bytes, ref int offset)
        {
            if (_byteSerializer.Deserialize(bytes, ref offset) == NullFlag.Null)
            {
                return null;
            }

            var byteCount = _intSerializer.Deserialize(bytes, ref offset);
            var value = _encoding.GetString(bytes, offset, byteCount);
            offset += byteCount;
            return value;
        }
    }
}