using Faker.Core.Interfaces;
using Faker.Tests.TestClasses;
using Faker.Core.Services;
using Faker.Core.Generators;

namespace Faker.Core.Tests
{
    public class Tests
    {
        private IFaker _faker;

        [SetUp]
        public void Setup()
        {
            _faker = new Services.Faker();
        }

        [Test]
        [TestCase(typeof(byte))]
        [TestCase(typeof(short))]
        [TestCase(typeof(int))]
        [TestCase(typeof(long))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        [TestCase(typeof(string))]
        [TestCase(typeof(bool))]
        [TestCase(typeof(char))]
        [TestCase(typeof(A))]
        public void CreatePrimitiveTest(Type type)
        {
            Assert.DoesNotThrow(() => _faker.Create(type));
        }

        [Test]
        [TestCase(typeof(byte))]
        [TestCase(typeof(short))]
        [TestCase(typeof(int))]
        [TestCase(typeof(long))]
        [TestCase(typeof(float))]
        [TestCase(typeof(double))]
        [TestCase(typeof(decimal))]
        [TestCase(typeof(string))]
        [TestCase(typeof(bool))]
        [TestCase(typeof(char))]
        public void CreatePrimitiveNotDefaultValue(Type type)
        {
            Assert.That(_faker.Create(type), Is.Not.EqualTo(UserTypeGenerator.GetDefaultValue(type)));
        }

        [Test]
        public void CreateInitedUserType()
        {
            A testClass = _faker.Create<A>();

            Assert.Multiple(() =>
            {
                Assert.NotZero(testClass.Id);
                Assert.NotZero(testClass.Value);
                Assert.NotNull(testClass.Name);
                Assert.NotNull(testClass.b);
                Assert.That('\0', Is.Not.EqualTo(testClass.b.symbol));
            });

        }
    }
}