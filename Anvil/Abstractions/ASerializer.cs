namespace Anvil.Abstractions
{
    public abstract class ASerializer
    {
        public abstract void SerializeObject(object value, byte[] bytes, ref int offset);
        public abstract object DeserializeObject(byte[] bytes, ref int offset);
    }
}