using System;

namespace Forum.Backend.Core.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base($"No user found with e-mail and password combination")
        {
        }
    }
}
