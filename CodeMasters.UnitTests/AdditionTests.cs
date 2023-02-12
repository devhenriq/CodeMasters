using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class AdditionTests : UnitTest
    {

        [Fact(DisplayName = "On addition calculate, should return left summed right")]
        public void CalculateShouldReturnAddedNumbers()
        {
            //Arrange
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left + right;

            var addition = new Addition();

            //Act
            var result = addition.Calculate(left, right);

            //Assert
            result.Should().Be(expected);
        }
    }
}