using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class MultiplicationTests : UnitTest

    {
        [Fact(DisplayName = "On multiplication calculate, should return left multiplied by right")]
        public void CalculateShouldReturnMultipliedResult()
        {
            //Arrange
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left * right;

            var multiplication = new Multiplication();

            //Act
            var result = multiplication.Calculate(left, right);

            //Assert
            result.Should().Be(expected);
        }
    }
}
