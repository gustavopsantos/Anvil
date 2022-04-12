namespace Anvil.Abstractions
{
    public abstract class AGenericSerializer<T> : ASerializer
    {
        public abstract void Serialize(T value, byte[] bytes, ref int offset);
        public abstract T Deserialize(byte[] bytes, ref int offset);

        public override void SerializeObject(object value, byte[] bytes, ref int offset)
        {
            Serialize((T) value, bytes, ref offset);
        }

        public override object DeserializeObject(byte[] bytes, ref int offset)
        {
            return Deserialize(bytes, ref offset);
        }
    }
}