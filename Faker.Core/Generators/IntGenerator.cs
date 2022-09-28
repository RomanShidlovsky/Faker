using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Generators
{
    public class IntGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.Next(1, int.MaxValue);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(int);
        }
    }
}
