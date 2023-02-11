namespace CodeMasters.Infrastructure.HttpClients
{
    public record SubmitTaskRequest
    {
        public SubmitTaskRequest(Guid id, decimal result)
        {
            Id = id;
            Result = result;
        }

        public Guid Id { get; }
        public decimal Result { get; }
    }
}
