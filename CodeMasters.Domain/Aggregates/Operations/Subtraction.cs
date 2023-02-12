using CodeMasters.Domain.Aggregates.Operations;

namespace CodeMasters.Domain.Entities.Operations
{
    public class Subtraction : IOperation
    {
        public double Calculate(double left, double right) => left - right;

    }
}
