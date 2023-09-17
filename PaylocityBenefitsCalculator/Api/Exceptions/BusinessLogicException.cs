namespace Api.Exceptions
{
    public class BusinessLogicException : PaylocityCustomException
    {
        public BusinessLogicException(string message) : base(message)
        {
        }

        public BusinessLogicException(string code, string message) : base(code, message)
        {
        }

        public BusinessLogicException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        public BusinessLogicException(string code, string message, Exception innerException)
            : base(code, message, innerException)
        {
        }
    }
}