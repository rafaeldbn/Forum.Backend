using System;

namespace Forum.Backend.Core.Exceptions
{
    public class DuplicateEmailException : Exception
    {
        public DuplicateEmailException(string email) : base($"Already exists a user with e-mail {email}")
        {
        }
    }
}
