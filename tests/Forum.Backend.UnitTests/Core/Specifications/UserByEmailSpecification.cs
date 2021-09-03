using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.Entities.UserAggregate.Specifications;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Forum.Backend.UnitTests.Core.Specifications
{
    public class UserByEmailSpecification
    {
        [Fact]
        public void FindUserByEmail()
        {
            var emailSearch = "test@email.com";
            var user1 = new User("test", emailSearch, "123456", "test");
            var user2 = new User("test", "test1@email.com", "123456", "test");
            var user3 = new User("test", "test2@email.com", "123456", "test");

            var users = new List<User>() { user1, user2, user3 };

            var spec = new UserByEmailSpec(emailSearch);
            var filteredList = users
                .Where(spec.WhereExpressions.Single().Compile())
                .ToList();

            Assert.Contains(user1, filteredList);
            Assert.DoesNotContain(user2, filteredList);
            Assert.DoesNotContain(user3, filteredList);
        }

        [Fact]
        public void ShouldNotFindUser()
        {
            var user1 = new User("test", "test@email.com", "123456", "test");
            var user2 = new User("test", "test1@email.com", "123456", "test");
            var user3 = new User("test", "test2@email.com", "123456", "test");

            var users = new List<User>() { user1, user2, user3 };

            var spec = new UserByEmailSpec("aaa@email.com");
            var filteredList = users
                .Where(spec.WhereExpressions.Single().Compile())
                .ToList();

            Assert.DoesNotContain(user1, filteredList);
            Assert.DoesNotContain(user2, filteredList);
            Assert.DoesNotContain(user3, filteredList);
        }
    }
}
