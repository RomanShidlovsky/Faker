using Faker.Core.Context;
using Faker.Core.Exceptions;
using Faker.Core.Interfaces;

namespace Faker.Core.Generators
{
    public class UserTypeGenerator : IValueGenerator
    {
        private Stack<Type> _createdTypes = new Stack<Type>();
        private readonly int _recursiveLimit = 1;
        public object Generate(Type typeToGenerate, GeneratorContext context)
        {
            object Obj = CreateObject(typeToGenerate, context);
            _createdTypes.Push(typeToGenerate);
            InitFields(Obj, typeToGenerate, context);
            InitProperties(Obj, typeToGenerate, context);
            _createdTypes.Pop();
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
            try
            {
                var obj = Activator.CreateInstance(typeToCreate);
                if (obj != null)
                    return obj;
            }
            catch { }
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
                        if (!CanInit(field.FieldType))
                            continue;
                        
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
                        if (!CanInit(property.PropertyType))
                            continue;

                        property.SetValue(objectToInit, context.Faker.Create(property.PropertyType));
                    }
                }
                catch
                { }
            }
        }

        private bool CanInit(Type type)
        {
            return _createdTypes.Where(p => p == type)
                .Count() <= _recursiveLimit;
        }

        public static  object? GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
