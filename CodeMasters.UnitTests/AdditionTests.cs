using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class AdditionTests : UnitTest
    {

        [Fact]
        public void CalculateShouldReturnAddedNumbers()
        {
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left + right;

            var addition = new Addition();

            var result = addition.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}