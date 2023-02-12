using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class MultiplicationTests : UnitTest

    {
        [Fact]
        public void CalculateShouldReturnMultipliedResult()
        {
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left * right;

            var multiplication = new Multiplication();

            var result = multiplication.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
