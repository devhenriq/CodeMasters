using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.UnitTests
{
    public class RemainderTests : UnitTest
    {
        [Fact]
        public void CalculateShouldReturnDivideByZeroException()
        {
            var left = _faker.Random.Double();
            const double right = 0;

            var remainder = new Remainder();

            var onCalculate = () => remainder.Calculate(left, right);
            onCalculate.Should().Throw<DivideByZeroException>();
        }

        [Fact]
        public void CalculateShouldReturnDividedValue()
        {
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left % right;
            var remainder = new Remainder();

            var result = remainder.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
