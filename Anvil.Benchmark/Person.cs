using System;

namespace Anvil.Benchmark
{
    [Serializable] // Required by .NET BinaryFormatter 
    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}