using Ardalis.GuardClauses;
using Forum.Backend.Core.PostAggregate;
using Forum.Backend.SharedKernel;
using Forum.Backend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;

namespace Forum.Backend.Core.Entities.UserAggregate
{
    public class User : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PasswordSalt { get; private set; }
        public DateTime? LastLoginDateUtc { get; private set; }
        public string TimeZone { get; private set; } = "E. South America Standard Time";

        public virtual ICollection<Post> Posts { get; set; }

        public User(string name, string email, string password, string timeZone)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
            Email = Guard.Against.NullOrEmpty(email, nameof(email));
            Password = Guard.Against.NullOrEmpty(password, nameof(password));
            TimeZone = Guard.Against.NullOrEmpty(timeZone, nameof(timeZone));
        }

        public void SetPasswordHash(string passwordHash, string passwordSalt)
        {
            Password = Guard.Against.NullOrEmpty(passwordHash, nameof(passwordHash));
            PasswordSalt = Guard.Against.NullOrEmpty(passwordSalt, nameof(passwordSalt));
        }

        public void UpdateName(string name)
        {
            Name = Guard.Against.NullOrEmpty(name, nameof(name));
        }

        public void UpdateEmail(string email)
        {
            Email = Guard.Against.NullOrEmpty(email, nameof(email));
        }
    }
}
