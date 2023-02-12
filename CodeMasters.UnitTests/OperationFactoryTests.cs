using CodeMasters.Domain.Aggregates.Operations;
using CodeMasters.Domain.Entities.Operations;
using CodeMasters.Domain.Factories;

namespace CodeMasters.UnitTests
{
    public class OperationFactoryTests
    {
        [Fact(DisplayName = "On operation create with invalid input should throws ArgumentException")]
        public void CreateOperationShouldReturnArgumentException()
        {
            //Arrange
            var factory = new OperationFactory();

            //Act
            var onCreate = () => factory.CreateOperation(string.Empty);

            //Assert
            onCreate.Should().Throw<ArgumentException>().WithMessage("Operation don't exists. (Parameter 'operation')");
        }

        [Theory(DisplayName = "On operation create with operation should return correct operation class")]
        [InlineData(OperationTypes.Addition, nameof(Addition))]
        [InlineData(OperationTypes.Multiplication, nameof(Multiplication))]
        [InlineData(OperationTypes.Subtraction, nameof(Subtraction))]
        [InlineData(OperationTypes.Division, nameof(Division))]
        [InlineData(OperationTypes.Remainder, nameof(Remainder))]
        public void CreateOperationShouldReturnConcreteOperation(string operation, string operationClassName)
        {
            //Arrange
            var factory = new OperationFactory();

            //Act
            var concreteOperation = factory.CreateOperation(operation);

            //Assert
            var type = concreteOperation.GetType();
            type.Name.Should().Be(operationClassName);
        }

    }
}
