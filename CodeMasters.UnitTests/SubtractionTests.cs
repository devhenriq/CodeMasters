using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.UnitTests
{
    public class SubtractionTests : UnitTest
    {
        [Fact(DisplayName = "On subtraction calculate, should return left subtracted by right")]
        public void CalculateShouldReturnSubtractedResult()
        {
            //Arrange
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left - right;

            var subtraction = new Subtraction();

            //Act
            var result = subtraction.Calculate(left, right);

            //Assert
            result.Should().Be(expected);
        }
    }
}
