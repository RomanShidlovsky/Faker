using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core.Generators
{
    public class CharGenerator : IValueGenerator
    {
        private static char[] _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-=+~`'".ToCharArray();

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            return _chars[context.Random.Next(_chars.Length)];
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(char);
        }
    }
}
