using System.Runtime.Serialization;

namespace CodeMasters.Domain.Exceptions
{
    [Serializable]
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string action) : base($"Invalid inputs on {action}")
        {

        }


        protected InvalidInputException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
