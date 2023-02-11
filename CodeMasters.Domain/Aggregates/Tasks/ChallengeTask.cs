namespace CodeMasters.Domain.Entities
{
    public record ChallengeTask
    {
        public ChallengeTask(Guid id, string operation, decimal left, decimal right)
        {
            Id = id;
            Operation = operation;
            Left = left;
            Right = right;
        }

        public Guid Id { get; }
        public string Operation { get; }
        public decimal Left { get; }
        public decimal Right { get; }
    }
}
