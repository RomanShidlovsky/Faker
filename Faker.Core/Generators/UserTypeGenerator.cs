using Faker.Core.Context;
using Faker.Core.Exceptions;
using Faker.Core.Interfaces;

namespace Faker.Core.Generators
{
    public class UserTypeGenerator : IValueGenerator
    {
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            object Obj = CreateObject(typeToGenerate, context);
            InitFields(Obj, typeToGenerate, context);
            InitProperties(Obj, typeToGenerate, context);

            return Obj;
        }

        public bool CanGenerate(Type type)
        {
            return type.IsClass || (type.IsValueType && !type.IsEnum);
        }

        private object CreateObject(Type typeToCreate, GeneratorContext context)
        {
            var constructors = typeToCreate.GetConstructors()
                .OrderByDescending(c => c.GetParameters().Length)
                .ToArray();

            foreach (var constructor in constructors)
            {
                try
                {
                    var parameters = constructor.GetParameters().
                        Select(p => context.Faker.Create(p.ParameterType))
                        .ToArray();

                    return constructor.Invoke(parameters);
                }
                catch
                { }
            }
            throw new TypeException($"Can't create instance of {typeToCreate.Name}", typeToCreate);
        }

        private void InitFields(object objectToInit, Type typeToInit, GeneratorContext context)
        {
            var fields = typeToInit.GetFields()
                .Where(f => !f.IsInitOnly);
            
            foreach (var field in fields)
            {
                try
                {
                    if (Equals(field.GetValue(objectToInit), GetDefaultValue(field.FieldType)))
                    {
                        field.SetValue(objectToInit, context.Faker.Create(field.FieldType));
                    }
                }
                catch
                { }
            }
        }

        private void InitProperties(object objectToInit, Type typeToInit, GeneratorContext context)
        {
            var properties = typeToInit.GetProperties()
                .Where(p => p.CanWrite);

            foreach (var property in properties)
            {
                try
                {
                    if (Equals(property.GetValue(objectToInit), GetDefaultValue(property.PropertyType)))
                    {
                        property.SetValue(objectToInit, context.Faker.Create(property.PropertyType));
                    }
                }
                catch
                { }
            }
        }

        public static object? GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
