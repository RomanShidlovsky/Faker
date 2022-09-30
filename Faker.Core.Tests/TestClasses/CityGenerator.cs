using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Tests.TestClasses
{
    public class CityGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return "Minsk";
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(string);
        }
    }
}
