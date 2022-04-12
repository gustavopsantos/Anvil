using Anvil.Abstractions;

namespace Anvil.Serializers
{
    public class FallbackSerializer<T> : AGenericSerializer<T>
    {
        public override void Serialize(T value, byte[] bytes, ref int offset)
        {
        }

        public override T Deserialize(byte[] bytes, ref int offset)
        {
            return default;
        }
    }
}