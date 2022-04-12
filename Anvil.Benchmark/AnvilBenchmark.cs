using System;
using Anvil.Serialization;
using Anvil.Utilities;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Newtonsoft.Json;

namespace Anvil.Benchmark
{
    [RankColumn]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class AnvilBenchmark
    {
        private const int Number = 42;
        private Serializer _anvilSerializer;

        [GlobalSetup]
        public void Setup()
        {
            var logger = new Logger(Console.WriteLine, Console.WriteLine, Console.WriteLine);
            _anvilSerializer = new Serializer(logger);
        }
        
        [Benchmark(Baseline = true)]
        public void JsonDotNet()
        {
            var serialized = JsonConvert.SerializeObject(Number);
            var deserialized = JsonConvert.DeserializeObject<int>(serialized);
        }

        [Benchmark]
        public void Anvil()
        {
            var serialized = _anvilSerializer.Serialize(Number);
            var deserialized = _anvilSerializer.Deserialize(serialized);
        }

        [Benchmark]
        public void BinaryFormatter()
        {
            var serialized = BinaryFormatterFacade.Serialize(Number);
            var deserialized = BinaryFormatterFacade.Deserialize(serialized);
        }
    }
}