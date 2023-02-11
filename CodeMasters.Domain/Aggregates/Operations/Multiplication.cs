namespace CodeMasters.Domain.Entities.Operations
{
    public class Multiplication : IOperation
    {
        public double Calculate(double left, double right) => left * right;
    }
}
