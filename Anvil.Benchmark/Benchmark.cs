using System;
using Anvil.Serialization;
using Anvil.Utilities;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using BinaryPack;
using Newtonsoft.Json;

namespace Anvil.Benchmark
{
    [RankColumn]
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    public class Benchmark
    {
        private static readonly Person Target = new Person {Name = "Willy Wonka", Age = 56};
        private Serializer _anvilSerializer;
        private byte[] _anvilSerialized;
        private byte[] _binaryFormatterSerialized;
        private string _newtonsoftSerialized;
        private byte[] _binaryPackSerialized;

        [GlobalSetup]
        public void Setup()
        {
            var logger = new Logger(Console.WriteLine, Console.WriteLine, Console.WriteLine);
            _anvilSerializer = new Serializer(logger);
            _anvilSerializer.SerializationModel.Add<Person>(new PersonSerializer(_anvilSerializer.SerializationModel));
            _anvilSerialized = _anvilSerializer.Serialize(Target);
            _binaryFormatterSerialized = BinaryFormatterFacade.Serialize(Target);
            _newtonsoftSerialized = JsonConvert.SerializeObject(Target);
            _binaryPackSerialized = BinaryConverter.Serialize(Target);
        }

        [Benchmark(Baseline = true)]
        public void NewtonsoftJsonSerialization()
        {
            JsonConvert.SerializeObject(Target);
        }

        [Benchmark]
        public void NewtonsoftJsonDeserialization()
        {
            JsonConvert.DeserializeObject<Person>(_newtonsoftSerialized);
        }

        [Benchmark]
        public void BinaryFormatterSerialization()
        {
            BinaryFormatterFacade.Serialize(Target);
        }

        [Benchmark]
        public void BinaryFormatterDeserialization()
        {
            BinaryFormatterFacade.Deserialize(_binaryFormatterSerialized);
        }
        
        [Benchmark]
        public void BinaryPackSerialization()
        {
            BinaryConverter.Serialize(Target);
        }

        [Benchmark]
        public void BinaryPackDeserialization()
        {
            BinaryConverter.Deserialize<Person>(_binaryPackSerialized);
        }

        [Benchmark]
        public void AnvilSerialization()
        {
            _anvilSerializer.Serialize(Target);
        }

        [Benchmark]
        public void AnvilDeserialization()
        {
            _anvilSerializer.Deserialize(_anvilSerialized);
        }
    }
}