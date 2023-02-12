using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.UnitTests
{
    public class SubtractionTests : UnitTest
    {
        [Fact]
        public void CalculateShouldReturnSubtractedResult()
        {
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left - right;

            var subtraction = new Subtraction();

            var result = subtraction.Calculate(left, right);
            result.Should().Be(expected);
        }
    }
}
