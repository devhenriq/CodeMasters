namespace CodeMasters.Domain.Entities.Operations
{
    public class Addition : IOperation
    {
        public decimal Calculate(decimal left, decimal right) => left + right;
    }
}
