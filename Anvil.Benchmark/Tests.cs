using System;
using Anvil.Serialization;
using Anvil.Utilities;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Anvil.Benchmark
{
    public class Tests
    {
        private static readonly int BenchmarkObject = 42;

        [Test]
        public void Ensure_Anvil_CanSerializeTargetBenchmarkObject()
        {
            var logger = new Logger(Console.WriteLine, Console.WriteLine, Console.WriteLine);
            var serializer = new Serializer(logger);
            var serialized = serializer.Serialize(BenchmarkObject);
            var deserialized = serializer.Deserialize(serialized);
            deserialized.GetType().Should().Be(BenchmarkObject.GetType());
            deserialized.Should().BeEquivalentTo(BenchmarkObject);
        }

        [Test]
        public void Ensure_JsonDotNet_CanSerializeTargetBenchmarkObject()
        {
            var serialized = JsonConvert.SerializeObject(BenchmarkObject);
            var deserialized = JsonConvert.DeserializeObject(serialized, BenchmarkObject.GetType());
            deserialized.GetType().Should().Be(BenchmarkObject.GetType());
            deserialized.Should().BeEquivalentTo(BenchmarkObject);
        }

        [Test]
        public void Ensure_BinaryFormatter_CanSerializeTargetBenchmarkObject()
        {
            var serialized = BinaryFormatterFacade.Serialize(BenchmarkObject);
            var deserialized = BinaryFormatterFacade.Deserialize(serialized);
            deserialized.GetType().Should().Be(BenchmarkObject.GetType());
            deserialized.Should().BeEquivalentTo(BenchmarkObject);
        }
    }
}