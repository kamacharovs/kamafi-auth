using System;
using System.Net;

namespace kamafi.auth.data.exceptions
{
    public class AuthFriendlyException : AuthException
    {
        public AuthFriendlyException()
        { }

        public AuthFriendlyException(string message)
            : base(message)
        { }

        public AuthFriendlyException(HttpStatusCode statusCode, string message)
            : base(statusCode, message)
        { }

        public AuthFriendlyException(string message, Exception inner)
            : base(message, inner)
        { }
    }
}
