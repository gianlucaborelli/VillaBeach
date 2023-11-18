namespace Api.Service.Exceptions
{
    [Serializable]
    public class AuthenticationServiceException : Exception
    {
        public int StatusCode { get; } 

        public AuthenticationServiceException() { }

        public AuthenticationServiceException(string message)
            : base(message) { }

        public AuthenticationServiceException(string message, Exception inner)
            : base(message, inner) { }

        public AuthenticationServiceException(string message, int statusCode)
            : this(message)
        {
            StatusCode = statusCode;
        }
    }
}