using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Generators
{
    public class ByteGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(1, byte.MaxValue);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(byte);
        }
    }
}
