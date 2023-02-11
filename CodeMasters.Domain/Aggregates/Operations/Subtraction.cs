namespace CodeMasters.Domain.Entities.Operations
{
    public class Subtraction : IOperation
    {
        public decimal Calculate(decimal left, decimal right) => left - right;

    }
}
