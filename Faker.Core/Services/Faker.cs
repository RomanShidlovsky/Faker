using Faker.Core.Interfaces;
using Faker.Core.Context;
using Faker.Core.Exceptions;
using Faker.Core.Generators;

namespace Faker.Core.Services
{
    public class Faker : IFaker
    {
        private readonly GeneratorContext _generatorContext;
        private readonly List<IValueGenerator> _valueGenerators;
        public IFakerConfig? Config { get; }

        public Faker()
        {
            _generatorContext = new GeneratorContext(
                new Random((int)DateTime.Now.Ticks & 0x0000FFFF),
                this
             );
            _valueGenerators = GetAllGenerators();
        }

        public Faker(IFakerConfig config)
            :this()
        {
            Config = config;
        }

        private static List<IValueGenerator> GetAllGenerators()
        {
            return new List<IValueGenerator>()
            {
                new BoolGenerator(),
                new ByteGenerator(),
                new CharGenerator(),
                new DateTimeGenerator(),
                new DecimalGenerator(),
                new DoubleGenerator(),
                new FloatGenerator(),
                new IntGenerator(),
                new LongGenerator(),
                new ShortGenerator(),
                new StringGenerator(),
                new ListGenerator(),
                new UserTypeGenerator()
            };
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
            throw new TypeException($"Can't create instance of {type.Name}", type);
        }

        public object CreateByName(Type type, string name)
        {
            if (Config != null)
            {
                var  generator = Config.GetGenerator(name);
                if (generator != null)
                {
                    return generator.Generate(type, _generatorContext);
                }
                
            }
            return CreateInstance(type);
        }
    }
}
