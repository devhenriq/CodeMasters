using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.UnitTests
{
    public class RemainderTests
    {
        private readonly Faker _faker;
        public RemainderTests()
        {
            _faker = new Faker();
        }
        [Fact]
        public void CalculateShouldReturnDivideByZeroException()
        {
            var left = _faker.Random.Double();
            var right = 0;

            var remainder = new Remainder();

            var onCalculate = () => remainder.Calculate(left, right);
            onCalculate.Should().Throw<DivideByZeroException>();
        }

        [Fact]
        public void CalculateShouldReturnDividedValue()
        {
            var left = 10;
            var right = 6;
            var expected = 4;
            var remainder = new Remainder();

            var result = remainder.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
