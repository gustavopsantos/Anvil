using Anvil.Abstractions;

namespace Anvil.Serializers
{
    public class ByteSerializer : AGenericSerializer<byte>
    {
        public override void Serialize(byte value, byte[] bytes, ref int offset)
        {
            bytes[offset] = value;
            offset += sizeof(byte);
        }

        public override byte Deserialize(byte[] bytes, ref int offset)
        {
            var value = bytes[offset];
            offset += sizeof(byte);
            return value;
        }
    }
}