using Forum.Backend.Infrastructure.Data;
using Forum.Backend.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Forum.Backend.Infrastructure
{
    public static class StartupSetup
    {
        public static void AddDbContext(this IServiceCollection services, string connectionString) =>
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connectionString)); // will be created in web project root

        public static void ConfigureJwt(this IServiceCollection services, IConfiguration configuration) => JwtStartupSetup.RegisterJWT(services, configuration);
    }
}
