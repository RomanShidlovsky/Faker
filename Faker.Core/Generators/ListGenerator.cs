using Faker.Core.Interfaces;
using Faker.Core.Exceptions;
using Faker.Core.Context;
using System.Collections;

namespace Faker.Core.Generators
{
    public class ListGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            var listSize = context.Random.Next(5, 20);
            var list = (IList?)Activator.CreateInstance(typeToGenerate, listSize);
            if (list != null)
            {
                for (int i = 0; i < listSize; i++)
                {
                    list.Add(context.Faker.Create(typeToGenerate.GetGenericArguments()[0]));
                }
                return list;
            }
            throw new TypeException($"Can't generate instance of{typeToGenerate.Name}", typeToGenerate);
        }

        public bool CanGenerate(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>) ;
        }
    }
}