namespace CodeMasters.Domain.Entities.Operations
{
    public interface IOperation
    {
        double Calculate(double left, double right);
    }
}
