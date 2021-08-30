using Forum.Backend.Core.Entities.CategoryAggregate;
using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.PostAggregate;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.IntegrationTests.Data
{
    public class EfRepositoryDelete : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task DeletesUserAfterAddingIt()
        {
            var repository = GetUserRepository();
            var initialName = Guid.NewGuid().ToString();
            var user = new User(initialName, "Test", "123456", "Test");
            await repository.AddAsync(user);

            // delete the item
            await repository.DeleteAsync(user);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync(),
                user => user.Name == initialName);
        }

        [Fact]
        public async Task DeletesCategoryAfterAddingIt()
        {
            var repository = GetCategoryRepository();
            var initialName = Guid.NewGuid().ToString();
            var category = new Category(initialName);
            await repository.AddAsync(category);

            // delete the item
            await repository.DeleteAsync(category);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync(),
                category => category.Name == initialName);
        }

        [Fact]
        public async Task DeletesPostAfterAddingIt()
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

            // delete the item
            await repository.DeleteAsync(post);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync(),
                post => post.Title == testTitle);
        }
    }
}
