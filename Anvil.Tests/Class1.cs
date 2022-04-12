using System;
using Anvil.Serialization;
using Anvil.Utilities;
using FluentAssertions;
using NUnit.Framework;

namespace Anvil.Tests
{
    public class Class1
    {
        private Serializer _serializer;

        [SetUp]
        public void Setup()
        {
            var logger = new Logger(Console.WriteLine, Console.WriteLine, Console.WriteLine);
            _serializer = new Serializer(logger);
        }

        [Test]
        public void Pineapple()
        {
            var serialized = _serializer.Serialize(42);
            var deserialized = _serializer.Deserialize(serialized);
            deserialized.GetType().Should().Be<int>();
            deserialized.Should().BeEquivalentTo(42);
        }
    }
}