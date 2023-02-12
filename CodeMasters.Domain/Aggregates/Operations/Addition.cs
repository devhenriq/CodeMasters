namespace CodeMasters.Domain.Aggregates.Operations
{
    public class Addition : IOperation
    {
        public double Calculate(double left, double right) => left + right;
    }
}
