using Anvil.Abstractions;
using Anvil.Serialization;
using Anvil.Utilities;

namespace Anvil.Benchmark
{
    public class PersonSerializer : AGenericSerializer<Person>
    {
        private readonly AGenericSerializer<byte> _byteSerializer;
        private readonly AGenericSerializer<string> _stringSerializer;
        private readonly AGenericSerializer<int> _intSerializer;

        public PersonSerializer(SerializationModel serializationModel)
        {
            _byteSerializer = serializationModel.Get<byte>();
            _stringSerializer = serializationModel.Get<string>();
            _intSerializer = serializationModel.Get<int>();
        }

        public override void Serialize(Person value, byte[] bytes, ref int offset)
        {
            if (value == null)
            {
                _byteSerializer.Serialize(NullFlag.Null, bytes, ref offset);
            }
            else
            {
                _byteSerializer.Serialize(NullFlag.NotNull, bytes, ref offset);
                _stringSerializer.Serialize(value.Name, bytes, ref offset);
                _intSerializer.Serialize(value.Age, bytes, ref offset);
            }
        }

        public override Person Deserialize(byte[] bytes, ref int offset)
        {
            var nullFlag = _byteSerializer.Deserialize(bytes, ref offset);

            if (nullFlag == NullFlag.Null)
            {
                return null;
            }

            return new Person
            {
                Name = _stringSerializer.Deserialize(bytes, ref offset),
                Age = _intSerializer.Deserialize(bytes, ref offset),
            };
        }
    }
}