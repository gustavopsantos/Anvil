using System;
using Anvil.Abstractions;
using Anvil.Serialization;

namespace Anvil.Serializers
{
    public class IntSerializer : AGenericSerializer<int>
    {
        public override void Serialize(int value, byte[] bytes, ref int offset)
        {
            FastBitConverter.GetBytes(value, bytes, offset);
            offset += sizeof(int);
        }

        public override int Deserialize(byte[] bytes, ref int offset)
        {
            var value = BitConverter.ToInt32(bytes, offset);
            offset += sizeof(int);
            return value;
        }
    }
}