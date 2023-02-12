namespace CodeMasters.Domain.Aggregates.Operations
{
    public class Multiplication : IOperation
    {
        public double Calculate(double left, double right) => left * right;
    }
}
