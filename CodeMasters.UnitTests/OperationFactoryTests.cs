using CodeMasters.Domain.Aggregates.Operations;
using CodeMasters.Domain.Entities.Operations;
using CodeMasters.Domain.Factories;

namespace CodeMasters.UnitTests
{
    public class OperationFactoryTests
    {
        [Fact]
        public void CreateOperationShouldReturnArgumentException()
        {
            //Arrange
            var factory = new OperationFactory();

            //Act
            var onCreate = () => factory.CreateOperation(string.Empty);

            //Assert
            onCreate.Should().Throw<ArgumentException>().WithMessage("Operation don't exists. (Parameter 'operation')");
        }

        [Theory]
        [InlineData(OperationTypes.Addition, nameof(Addition))]
        [InlineData(OperationTypes.Multiplication, nameof(Multiplication))]
        [InlineData(OperationTypes.Subtraction, nameof(Subtraction))]
        [InlineData(OperationTypes.Division, nameof(Division))]
        [InlineData(OperationTypes.Remainder, nameof(Remainder))]
        public void CreateOperationShouldReturnAddition(string operation, string operationClassName)
        {
            //Arrange
            var factory = new OperationFactory();

            //Act
            var addition = factory.CreateOperation(operation);

            //Assert
            var type = addition.GetType();
            type.Name.Should().Be(operationClassName);
        }

    }
}
