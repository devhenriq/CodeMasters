﻿using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class DivisionTests
    {
        private readonly Faker _faker;
        public DivisionTests()
        {
            _faker = new Faker();
        }
        [Fact]
        public void CalculateShouldReturnDivideByZeroException()
        {
            var left = _faker.Random.Double();
            var right = 0;

            var division = new Division();

            var onCalculate = () => division.Calculate(left, right);
            onCalculate.Should().Throw<DivideByZeroException>();
        }

        [Fact]
        public void CalculateShouldReturnDividedValue()
        {
            var left = 10;
            var right = 5;
            var expected = 2;
            var division = new Division();

            var result = division.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
