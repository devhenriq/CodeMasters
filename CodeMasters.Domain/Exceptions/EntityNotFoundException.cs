using System.Runtime.Serialization;

namespace CodeMasters.Domain.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entity) : base($"{entity} not found.")
        {

        }


        protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
