using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.UnitTests
{
    public class AdditionTests
    {
        [Fact]
        public void CalculateShouldReturnAddedNumbers()
        {
            var left = 5;
            var right = 6;
            var expected = 11;

            var addition = new Addition();

            var result = addition.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}