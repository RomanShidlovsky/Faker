using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Generators
{
    public class BoolGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return true;
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(bool);
        }
    }
}