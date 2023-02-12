namespace CodeMasters.Domain.Aggregates.Operations
{
    public interface IOperation
    {
        double Calculate(double left, double right);
    }
}
