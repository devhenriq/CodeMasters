using CodeMasters.Domain.Aggregates.Operations;
using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.Domain.Factories
{
    public class OperationFactory : IOperationFactory
    {
        public IOperation CreateOperation(string operation)
        {
            return operation switch
            {
                OperationTypes.Addition => new Addition(),
                OperationTypes.Division => new Division(),
                OperationTypes.Multiplication => new Multiplication(),
                OperationTypes.Remainder => new Remainder(),
                OperationTypes.Subtraction => new Subtraction(),
                _ => throw new ArgumentException("Operation don't exists.", nameof(operation))
            };
        }
    }
}
