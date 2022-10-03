namespace Faker.Core.Tests.TestClasses
{
    public class C
    { 
        public D d { get; set; }
        public string s { get; set; }
    }

    public class D
    {
        public E e { get; set; }
    }

    public class E
    {
        public C c { get; set; }
    }
}
