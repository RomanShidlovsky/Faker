using Faker.Core.Interfaces;
using Faker.Core.Context;

namespace Faker.Core
{
    public class Faker : IFaker
    {
        private readonly GeneratorContext _generatorContext;
        private readonly List<IValueGenerator> _valueGenerators;

        public Faker()
        {
            _generatorContext = new GeneratorContext(
                new Random((int)DateTime.Now.Ticks & 0x0000FFFF),
                this
             );
            _valueGenerators = GetAllGenerators();
        }

        private static List<IValueGenerator> GetAllGenerators()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(t => t.GetInterfaces().Contains(typeof(IValueGenerator)) && t.IsClass)
                .ToList();

            List<IValueGenerator> generators = new List<IValueGenerator>();
            foreach (var type in types)
            {
                var generator = Activator.CreateInstance(type) as IValueGenerator;
                if (generator != null)
                {
                    generators.Add(generator);
                }
            }

            return generators;
        }

        public T Create<T>()
        {
            return (T)CreateInstance(typeof(T)); 
        }

        public object Create(Type type)
        {
            return CreateInstance(type);
        }

        private object CreateInstance(Type type)
        {
            foreach (var generator in _valueGenerators)
            {
                if (generator.CanGenerate(type))
                {
                    return generator.Generate(type, _generatorContext);
                }
            }
            throw new Exception($"Can't create instance of {type.Name}");
        }


    }
}
