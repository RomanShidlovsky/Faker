﻿using Faker.Core.Interfaces;

namespace Faker.Core
{
    public class GeneratorContext
    {
        public Random Random { get; }
        public IFaker Faker { get; }

        public GeneratorContext(Random random, IFaker faker)
        {
            Random = random;
            Faker = faker;
        }
    }
}
