using System.Runtime.CompilerServices;

namespace Anvil.Serialization
{
    internal static class FastBitConverter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void GetBytes(int value, byte[] buffer, int offset)
        {
            buffer[offset + 0] = (byte) (value >> 00);
            buffer[offset + 1] = (byte) (value >> 08);
            buffer[offset + 2] = (byte) (value >> 16);
            buffer[offset + 3] = (byte) (value >> 24);
        }
    }
}