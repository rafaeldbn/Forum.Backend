using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.Entities.UserAggregate.Events;
using Forum.Backend.Core.Interfaces;
using Forum.Backend.SharedKernel.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Backend.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _repository;
        private readonly ICryptoService _cryptoService;

        public UserService(IRepository<User> repository, ICryptoService cryptoService)
        {
            _repository = repository;
            _cryptoService = cryptoService;
        }

        public async Task<User> AddNewUserAsync(string name, string email, string password, string timeZone, CancellationToken cancellationToken)
        {
            var user = new User(name, email, password, timeZone);
            var salt = _cryptoService.CreateSalt();
            var passwordHash = _cryptoService.CreateHash(password, salt);

            user.SetPasswordHash(passwordHash, salt);

            await _repository.AddAsync(user, cancellationToken);

            user.Events.Add(new NewUserAddedEvent(user));

            return user;
        }
    }
}
