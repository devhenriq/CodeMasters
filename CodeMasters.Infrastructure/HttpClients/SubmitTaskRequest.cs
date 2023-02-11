namespace CodeMasters.Infrastructure.HttpClients
{
    public record SubmitTaskRequest
    {
        public SubmitTaskRequest(Guid id, double result)
        {
            Id = id;
            Result = result;
        }

        public Guid Id { get; }
        public double Result { get; }
    }
}
