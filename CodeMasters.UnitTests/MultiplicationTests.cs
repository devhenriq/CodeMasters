using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class MultiplicationTests

    {
        [Fact]
        public void CalculateShouldReturnMultipliedResult()
        {
            var left = 5;
            var right = 2;
            var expected = 10;

            var multiplication = new Multiplication();

            var result = multiplication.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
