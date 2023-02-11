namespace CodeMasters.Domain.Entities.Operations
{
    public class Remainder : IOperation
    {
        public decimal Calculate(decimal left, decimal right)
        {
            if (right == 0) throw new DivideByZeroException();
            return left % right;
        }
    }
}
