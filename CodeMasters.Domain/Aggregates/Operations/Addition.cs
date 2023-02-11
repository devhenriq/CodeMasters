namespace CodeMasters.Domain.Entities.Operations
{
    public class Addition : IOperation
    {
        public double Calculate(double left, double right) => left + right;
    }
}
