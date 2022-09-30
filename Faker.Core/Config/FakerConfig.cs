using Faker.Core.Interfaces;
using System.Linq.Expressions;

namespace Faker.Core.Config
{
    public class FakerConfig : IFakerConfig
    {
        private Dictionary<string, IValueGenerator> _generators = new Dictionary<string, IValueGenerator>();
        
        public void Add<T, M, G>(Expression<Func<T,M>> expression) where G : IValueGenerator
        {
            try
            {
                MemberExpression member = (MemberExpression)expression.Body;
                
                G? generator = (G)Activator.CreateInstance(typeof(G));
                if (generator != null)
                {
                    _generators.Add(typeof(T).Name+'.'+member.Member.Name.ToLower(), generator);
                }
            }
            catch
            {
                throw new Exception("Can't add generator to config");
            }
        }

        public IValueGenerator? GetGenerator(string name)
        {
            if (_generators.TryGetValue(name, out IValueGenerator generator))
            {
                return generator;
            }
            return null;
        }
    }
}
