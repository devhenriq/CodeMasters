using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.UnitTests
{
    public class RemainderTests : UnitTest
    {
        [Fact(DisplayName = "On remainder calculate, should throw DivideByZeroException when right is 0")]
        public void CalculateShouldReturnDivideByZeroException()
        {
            //Arrange
            var left = _faker.Random.Double();
            const double right = 0;

            var remainder = new Remainder();

            //Act
            var onCalculate = () => remainder.Calculate(left, right);

            //Assert
            onCalculate.Should().Throw<DivideByZeroException>();
        }

        [Fact(DisplayName = "On remainder calculate, should return the remainder of left divided by right")]
        public void CalculateShouldReturnDividedValue()
        {
            //Arrange
            var left = _faker.Random.Double();
            var right = _faker.Random.Double();
            var expected = left % right;
            var remainder = new Remainder();

            //Act
            var result = remainder.Calculate(left, right);

            //Assert
            result.Should().Be(expected);
        }
    }
}
