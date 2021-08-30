using Forum.Backend.Core.Entities.UserAggregate;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.IntegrationTests.Data
{
    public class EfRepositoryUpdate : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task UpdatesUserAfterAddingIt()
        {
            var repository = GetUserRepository();
            var testName = "test name";
            var testEmail = "test@test.com";
            var testPassword = "123456";
            var testTimezone = "E. South America Standard Time";
            var user = new User(testName, testEmail, testPassword, testTimezone);

            await repository.AddAsync(user);

            // detach the item so we get a different instance
            _dbContext.Entry(user).State = EntityState.Detached;

            // fetch the item and update its title
            var newUser = (await repository.ListAsync())
                .FirstOrDefault(user => user.Name == testName);

            Assert.NotNull(newUser);
            Assert.NotSame(user, newUser);

            var newName = Guid.NewGuid().ToString();
            newUser.UpdateName(newName);

            var newEmail = Guid.NewGuid().ToString();
            newUser.UpdateEmail(newEmail);

            // Update the item
            await repository.UpdateAsync(newUser);

            // Fetch the updated item
            var updatedItem = (await repository.ListAsync())
                .FirstOrDefault(project => project.Name == newName);

            Assert.NotNull(updatedItem);
            Assert.NotEqual(user.Name, updatedItem.Name);
            Assert.NotEqual(user.Email, updatedItem.Email);
            Assert.Equal(newUser.Id, updatedItem.Id);
        }
    }
}
