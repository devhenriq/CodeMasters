using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class DivisionTests : UnitTest
    {
        [Fact(DisplayName = "On division calculate, should throw DivideByZeroException when right is 0")]
        public void CalculateShouldReturnDivideByZeroException()
        {
            //Arrange
            var left = _faker.Random.Double();
            const double right = 0;

            var division = new Division();

            //Act
            var onCalculate = () => division.Calculate(left, right);

            //Assert
            onCalculate.Should().Throw<DivideByZeroException>();
        }

        [Fact(DisplayName = "On division calculate, should return left divided by right")]
        public void CalculateShouldReturnDividedValue()
        {
            //Arrange
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left / right;

            var division = new Division();

            //Act
            var result = division.Calculate(left, right);

            //Assert
            result.Should().Be(expected);
        }
    }
}
