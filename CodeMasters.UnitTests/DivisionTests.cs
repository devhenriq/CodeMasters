using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.UnitTests
{
    public class DivisionTests : UnitTest
    {
        [Fact]
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

        [Fact]
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
