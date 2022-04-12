using System;
using System.Collections.Generic;
using Anvil.Abstractions;
using Anvil.Extensions;
using Anvil.Serializers;
using Anvil.Utilities;

namespace Anvil.Serialization
{
    public class SerializationModel
    {
        private readonly Logger _logger;
        private readonly Dictionary<Type, ASerializer> _registry = new();

        public SerializationModel(Logger logger)
        {
            _logger = logger;
        }

        public ASerializer Get(Type type)
        {
            return _registry[type];
        }

        public AGenericSerializer<T> Get<T>()
        {
            var type = typeof(T);

            if (_registry.TryGetValue(type, out var serializer))
            {
                return (AGenericSerializer<T>) serializer;
            }

            _logger.Warning.Invoke($"Bypassing type '{typeof(T).GetFormattedName()}' serialization.");
            serializer = new FallbackSerializer<T>();
            _registry.Add(type, serializer);
            return (AGenericSerializer<T>) serializer;
        }

        public void Add<T>(AGenericSerializer<T> serializer)
        {
            _registry.Add(typeof(T), serializer);
        }
    }
}