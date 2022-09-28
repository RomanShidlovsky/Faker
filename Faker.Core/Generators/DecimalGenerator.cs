using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Generators
{
    public class DecimalGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            Random random = context.Random;
            byte scale = (byte)random.Next(29);
            bool sign = random.Next(2) == 1;
            return new decimal(random.Next(), random.Next(), 
                random.Next(), sign, scale);
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(decimal);
        }
    }
}
