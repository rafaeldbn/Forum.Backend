using Ardalis.GuardClauses;
using Forum.Backend.Core.Entities.CategoryAggregate;
using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.SharedKernel;
using Forum.Backend.SharedKernel.Interfaces;
using System;

namespace Forum.Backend.Core.PostAggregate
{
    public class Post : BaseEntity, IAggregateRoot
    {
        public Post(int userId, int categoryId, string title, string message)
        {
            UserId = Guard.Against.NegativeOrZero(userId, nameof(userId));
            CategoryId = Guard.Against.NegativeOrZero(categoryId, nameof(categoryId));
            CreatedAtUtc = DateTime.UtcNow;
            Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
            Message = Guard.Against.NullOrWhiteSpace(message, nameof(message));
        }

        public int UserId { get; private set; }
        public int CategoryId { get; private set; }
        public DateTime CreatedAtUtc { get; private set; }
        public DateTime? UpdatedAtUtc { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }

        public void UpdateMessage(string message)
        {
            Message = Guard.Against.NullOrWhiteSpace(message, nameof(message));
            UpdatedAtUtc = DateTime.UtcNow;
        }

        public void UpdateTitle(string title)
        {
            Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
            UpdatedAtUtc = DateTime.UtcNow;
        }
    }
}
