using System.Reflection;
using Faker.Core.Interfaces;
using Faker.Core.Tests.TestClasses;
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
        [TestCase(typeof(TestInit))]
        [TestCase(typeof(TestCtorStruct))]
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
            TestInit testClass = _faker.Create<TestInit>();

            Assert.Multiple(() =>
            {
                Assert.NotZero(testClass.Int);
                Assert.NotZero(testClass.Byte);
                Assert.NotNull(testClass.String);
                Assert.True(testClass.Bool);
                Assert.NotNull(testClass.b);
                Assert.That(testClass.b.symbol, Is.Not.EqualTo('\0'));
            });
        }

        [Test]
        public void CreateWithCycleDependencies()
        {
            Assert.DoesNotThrow(() => _faker.Create<C>());
        }
        
        [Test]
        public void CreateCycleParentInited()
        {
            C testClass = _faker.Create<C>();
            Assert.Multiple(() =>
            {
                Assert.NotNull(testClass.d.e.c);
                Assert.NotNull(testClass.d.e.c.s);
            });

        }

        [Test]
        public void CreateSelectConstructor()
        {
            TestCtor testClass = _faker.Create<TestCtor>();
            Assert.NotZero(testClass.C);
        }

        [Test]
        public void CreateTestStructCtor()
        {
            TestCtorStruct ctorStruct = _faker.Create<TestCtorStruct>();

            Assert.Multiple(() =>
            {
                Assert.NotNull(ctorStruct.String);
                Assert.NotZero(ctorStruct.Int);
            });
        }

        [Test]
        public void CreateTestInitStruct()
        {
            TestInitStruct initStruct = _faker.Create<TestInitStruct>();

            Assert.Multiple(() =>
            {
                Assert.NotZero(initStruct.Decimal);
                Assert.True(initStruct.Bool);
            });
        }
    }
}