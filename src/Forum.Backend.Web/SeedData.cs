using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Forum.Backend.Web
{
    public static class SeedData
    {
        public static readonly User TestUserGet = new User("Test User get", "testuserget@email.com", "123456", "timezone");
        public static readonly User TestUser = new User("Test User", "testuser@email.com", "123456", "timezone");

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
            {
                // Look for any TODO items.
                if (dbContext.Users.Any())
                {
                    return;   // DB has been seeded
                }

                PopulateTestData(dbContext);


            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            dbContext.Users.Add(TestUserGet);

            dbContext.SaveChanges();
        }
    }
}
