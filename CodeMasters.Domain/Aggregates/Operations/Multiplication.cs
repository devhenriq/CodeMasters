namespace CodeMasters.Domain.Entities.Operations
{
    public class Multiplication : IOperation
    {
        public decimal Calculate(decimal left, decimal right) => left * right;
    }
}
