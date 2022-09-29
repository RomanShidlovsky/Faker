namespace Faker.Core.Exceptions
{
    public class TypeException : Exception
    {
        public Type Type { get; }

        public TypeException(string message, Type type)
            :base(message)
        {
            Type = type;
        }
    }
}
