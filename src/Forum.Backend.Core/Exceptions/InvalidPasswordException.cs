using System;

namespace Forum.Backend.Core.Exceptions
{
    public class InvalidPasswordException : Exception
    {
        public InvalidPasswordException() : base($"No user found with e-mail and password combination")
        {
        }
    }
}
