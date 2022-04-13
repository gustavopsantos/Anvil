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

        private byte[] _anvilSerialized;
        private byte[] _binaryFormatterSerialized;
        private string _newtonsoftSerialized;

        [GlobalSetup]
        public void Setup()
        {
            var logger = new Logger(Console.WriteLine, Console.WriteLine, Console.WriteLine);
            _anvilSerializer = new Serializer(logger);
            _anvilSerialized = _anvilSerializer.Serialize(Number);
            _binaryFormatterSerialized = BinaryFormatterFacade.Serialize(Number);
            _newtonsoftSerialized = JsonConvert.SerializeObject(Number);
        }

        [Benchmark(Baseline = true)] public void NewtonsoftJsonSerialization() => JsonConvert.SerializeObject(Number);
        [Benchmark] public void NewtonsoftJsonDeserialization() => JsonConvert.DeserializeObject<int>(_newtonsoftSerialized);
        [Benchmark] public void BinaryFormatterSerialization() => BinaryFormatterFacade.Serialize(Number);
        [Benchmark] public void BinaryFormatterDeserialization() => BinaryFormatterFacade.Deserialize(_binaryFormatterSerialized);
        [Benchmark] public void AnvilSerialization() => _anvilSerializer.Serialize(Number);
        [Benchmark] public void AnvilDeserialization() => _anvilSerializer.Deserialize(_anvilSerialized);
    }
}