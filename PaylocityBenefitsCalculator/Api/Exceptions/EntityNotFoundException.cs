namespace Api.Exceptions
{
    public class EntityNotFoundException : PaylocityCustomException
    {
        public EntityNotFoundException(string message) : base(message)
        {
        }

        public EntityNotFoundException(string code, string message) : base(code, message)
        {
        }

        public EntityNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        public EntityNotFoundException(string code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }
    }
}