using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Generators
{
    public class CountryGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return "Belarus";
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }
    }
}
