namespace Api.Exceptions
{
    public abstract class PaylocityCustomException : Exception
    {
        public string? Code { get; }
        
        protected PaylocityCustomException(string message) : base(message)
        {
        }
        
        protected PaylocityCustomException(string code, string message) : base(message)
        {
            Code = code;
        }

        protected PaylocityCustomException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
        
        protected PaylocityCustomException(string code, string message, Exception innerException)
            : base(message, innerException)
        {
            Code = code;
        }
    }
}