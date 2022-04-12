using FluentAssertions;
using NUnit.Framework;

namespace Anvil.Tests
{
    public class Class1
    {
        [Test]
        public void TestOne()
        {
            1.Should().Be(1);
        }
        
        [Test]
        public void TestTwo()
        {
            1.Should().Be(2);
        }
    }
}