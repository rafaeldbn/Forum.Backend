using Forum.Backend.Core.Entities.CategoryAggregate;
using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.PostAggregate;
using Forum.Backend.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace Forum.Backend.IntegrationTests.Data
{
    public abstract class BaseEfRepoTestFixture
    {
        protected AppDbContext _dbContext;

        protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
        {
            // Create a fresh service provider, and therefore a fresh
            // InMemory database instance.
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            // Create a new options instance telling the context to use an
            // InMemory database and the new service provider.
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("cleanarchitecture")
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected EfRepository<User> GetUserRepository()
        {
            var options = CreateNewContextOptions();
            var mockMediator = new Mock<IMediator>();

            _dbContext = new AppDbContext(options, mockMediator.Object);
            return new EfRepository<User>(_dbContext);
        }

        protected EfRepository<Category> GetCategoryRepository()
        {
            var options = CreateNewContextOptions();
            var mockMediator = new Mock<IMediator>();

            _dbContext = new AppDbContext(options, mockMediator.Object);
            return new EfRepository<Category>(_dbContext);
        }

        protected EfRepository<Post> GetPostRepository()
        {
            var options = CreateNewContextOptions();
            var mockMediator = new Mock<IMediator>();

            _dbContext = new AppDbContext(options, mockMediator.Object);
            return new EfRepository<Post>(_dbContext);
        }
    }
}
