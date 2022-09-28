using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Generators
{
    public class FloatGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return context.Random.NextSingle();
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(float);
        }
    }
}
