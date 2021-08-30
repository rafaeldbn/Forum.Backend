using Ardalis.GuardClauses;
using Forum.Backend.Core.PostAggregate;
using Forum.Backend.SharedKernel;
using Forum.Backend.SharedKernel.Interfaces;
using System.Collections.Generic;

namespace Forum.Backend.Core.Entities.CategoryAggregate
{
    public class Category : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }

        public virtual ICollection<Post> Posts { get; set; }

        public Category(string name)
        {
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        }

        public void UpdateName(string name)
        {
            Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
        }
    }
}
