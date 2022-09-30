using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker.Core.Tests.TestClasses
{
    public class CtorThrows
    {
        public CtorThrows()
        {
            throw new Exception();
        }   
    }
}
