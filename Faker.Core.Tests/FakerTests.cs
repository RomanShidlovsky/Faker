using Faker.Core.Interfaces;
using Faker.Core;

namespace Faker.Core.Tests
{
    public class Tests
    {
        private IFaker _faker;

        [SetUp]
        public void Setup()
        {
            _faker = new Faker();
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
        public void CreatePrimitiveTest(Type type)
        {
            Assert.DoesNotThrow(() => _faker.Create(type));
        }

        [Test]
        public void CreatePrimitiveNotDefaultValue()
        {
            Assert.Multiple(() =>
            {
                Assert.NotZero(_faker.Create<byte>());
                Assert.NotZero(_faker.Create<short>());
                Assert.NotZero(_faker.Create<int>());
                Assert.NotZero(_faker.Create<long>());
                Assert.NotZero(_faker.Create<float>());
                Assert.NotZero(_faker.Create<double>());
                Assert.NotZero(_faker.Create<decimal>());
                Assert.NotNull(_faker.Create<string>());
                Assert.True(_faker.Create<bool>());
                Assert.That(_faker.Create<char>(), Is.Not.EqualTo('\0'));
            });
        }

    }
}