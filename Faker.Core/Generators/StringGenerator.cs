using Faker.Core.Interfaces;
using Faker.Core.Context;
using System.Text;

namespace Faker.Core.Generators
{
    public class StringGenerator : IValueGenerator
    {
        private static char[] _chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-=+~`'".ToCharArray();

        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            var random = context.Random;
            var length = random.Next(1, 100);
            var builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                builder.Append(_chars[random.Next(_chars.Length)]);
            }
            
            return builder.ToString();
        }

        public bool CanGenerate(Type type)
        {
            return type == typeof(char);
        }
    }
}
