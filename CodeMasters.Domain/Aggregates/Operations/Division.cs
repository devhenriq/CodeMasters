namespace CodeMasters.Domain.Aggregates.Operations
{
    public class Division : IOperation
    {

        public double Calculate(double left, double right)
        {
            if (right == 0) throw new DivideByZeroException();
            return left / right;
        }
    }
}