namespace CodeMasters.Domain.Entities
{
    public record ChallengeTask
    {
        public ChallengeTask(Guid id, string operation, double left, double right)
        {
            Id = id;
            Operation = operation;
            Left = left;
            Right = right;
        }

        public Guid Id { get; private set; }
        public string Operation { get; private set; }
        public double Left { get; private set; }
        public double Right { get; private set; }
        public double? Result { get; private set; }
        public void SetResult(double result)
        {
            Result = result;
        }
    }
}
