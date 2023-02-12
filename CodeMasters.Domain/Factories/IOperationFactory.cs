using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.Domain.Factories
{
    public interface IOperationFactory
    {
        IOperation CreateOperation(string operation);
    }
}
