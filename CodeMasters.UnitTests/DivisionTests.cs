using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class DivisionTests : UnitTest
    {
        [Fact]
        public void CalculateShouldReturnDivideByZeroException()
        {
            var left = _faker.Random.Double();
            const double right = 0;

            var division = new Division();

            var onCalculate = () => division.Calculate(left, right);
            onCalculate.Should().Throw<DivideByZeroException>();
        }

        [Fact]
        public void CalculateShouldReturnDividedValue()
        {
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left / right;

            var division = new Division();

            var result = division.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
