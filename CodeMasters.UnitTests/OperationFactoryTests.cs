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
            var factory = new OperationFactory();
            var onCreate = () => factory.CreateOperation(string.Empty);
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
            var factory = new OperationFactory();
            var addition = factory.CreateOperation(operation);
            var type = addition.GetType();
            type.Name.Should().Be(operationClassName);
        }

    }
}
