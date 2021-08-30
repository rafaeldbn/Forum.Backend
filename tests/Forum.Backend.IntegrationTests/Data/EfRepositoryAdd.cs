using Forum.Backend.Core.Entities.CategoryAggregate;
using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.PostAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.IntegrationTests.Data
{
    public class EfRepositoryAdd : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task AddsUserAndSetsId()
        {
            var testName = "test name";
            var testEmail = "test@test.com";
            var testPassword = "123456";
            var testTimezone = "E. South America Standard Time";

            var repository = GetUserRepository();
            var user = new User(testName, testEmail, testPassword, testTimezone);

            await repository.AddAsync(user);

            var newUser = (await repository.ListAsync())
                            .FirstOrDefault();

            Assert.Equal(testName, newUser.Name);
            Assert.Equal(testEmail, newUser.Email);
            Assert.Equal(testPassword, newUser.Password);
            Assert.Equal(testTimezone, newUser.TimeZone);
            Assert.True(newUser?.Id > 0);
        }

        [Fact]
        public async Task AddsCategoryAndSetsId()
        {
            var testName = "test name";

            var repository = GetCategoryRepository();
            var category = new Category(testName);

            await repository.AddAsync(category);

            var newCategory = (await repository.ListAsync())
                            .FirstOrDefault();

            Assert.Equal(testName, newCategory.Name);
            Assert.True(newCategory?.Id > 0);
        }

        [Fact]
        public async Task AddsPostAndSetsId()
        {
            var repository = GetPostRepository();
            var userRepository = GetUserRepository();
            var categoryRepository = GetCategoryRepository();

            await categoryRepository.AddAsync(new Category(Guid.NewGuid().ToString()));

            var newCategory = (await categoryRepository.ListAsync())
                            .FirstOrDefault();

            await userRepository.AddAsync(new User(Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString(), Guid.NewGuid().ToString()));

            var newUser = (await userRepository.ListAsync())
                            .FirstOrDefault();

            var testTitle = "Test title";
            var testMessage = "Test message";
            var post = new Post(newUser.Id, newCategory.Id, testTitle, testMessage);

            await repository.AddAsync(post);

            var newPost = (await repository.ListAsync())
                            .FirstOrDefault();

            Assert.Equal(testTitle, newPost.Title);
            Assert.Equal(testMessage, newPost.Message);
            Assert.True(newPost.CreatedAtUtc > DateTime.MinValue);
            Assert.True(newPost?.Id > 0);
        }



    }
}
