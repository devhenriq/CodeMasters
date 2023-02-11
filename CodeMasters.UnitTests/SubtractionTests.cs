using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.UnitTests
{
    public class SubtractionTests
    {
        [Fact]
        public void CalculateShouldReturnSubtractedResult()
        {
            var left = 5;
            var right = 6;
            var expected = -1;

            var subtraction = new Subtraction();

            var result = subtraction.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
