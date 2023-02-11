namespace CodeMasters.Domain.Entities.Operations
{
    public interface IOperation
    {
        decimal Calculate(decimal left, decimal right);
    }
}
