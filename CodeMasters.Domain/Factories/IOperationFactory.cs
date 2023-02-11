using CodeMasters.Domain.Entities.Operations;

namespace CodeMasters.Domain.Factories
{
    public interface IOperationFactory
    {
        IOperation CreateOperation(string operation);
    }
}
