using Forum.Backend.Core.Entities.UserAggregate;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Backend.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> AddNewUserAsync(string name, string email, string password, string timeZone, CancellationToken cancellationToken);
        Task<User> AuthenticationByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken);
    }
}
