using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Tests.TestClasses
{
    public class ConfigTest
    {
        public string City { get; }
        public int Age { get; set; }
        public string Country { get; set; }

        public ConfigTest(string city, int age)
        {
            City = city;
            Age = age;
        }
    }
}
