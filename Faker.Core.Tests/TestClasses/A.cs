using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Tests.TestClasses
{
    public class A
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte Value { get; set; }
        public B? b;

        public A(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
