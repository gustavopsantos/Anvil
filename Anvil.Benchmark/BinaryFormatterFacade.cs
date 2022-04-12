using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Anvil.Benchmark
{
    public static class BinaryFormatterFacade
    {
        private static readonly BinaryFormatter _binaryFormatter = new BinaryFormatter();

        public static byte[] Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                _binaryFormatter.Serialize(stream, obj);
                return stream.ToArray();
            }
        }

        public static object Deserialize(byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                return _binaryFormatter.Deserialize(stream);
            }
        }
    }
}