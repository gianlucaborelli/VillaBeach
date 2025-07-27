namespace Api.CrossCutting.Identity.Authentication.Exceptions
{
    [Serializable]
    public class AuthenticationException : Exception
    {
        public int StatusCode { get; } 

        public AuthenticationException() { }

        public AuthenticationException(string message)
            : base(message) { }

        public AuthenticationException(string message, Exception inner)
            : base(message, inner) { }

        public AuthenticationException(string message, int statusCode)
            : this(message)
        {
            StatusCode = statusCode;
        }
    }
}