using Ardalis.GuardClauses;
using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.Entities.UserAggregate.Events;
using Forum.Backend.Core.Entities.UserAggregate.Specifications;
using Forum.Backend.Core.Exceptions;
using Forum.Backend.Core.Extensions;
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
            Guard.Against.NullOrWhiteSpace(name, nameof(name));
            Guard.Against.NullOrWhiteSpace(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(password, nameof(password));
            Guard.Against.NullOrWhiteSpace(timeZone, nameof(timeZone));

            var userWithEmail = await _repository.GetBySpecAsync(new UserByEmailSpec(email));
            if (userWithEmail != null)
            {
                throw new DuplicateEmailException(email);
            }

            var user = new User(name, email, password, timeZone);
            var salt = _cryptoService.CreateSalt();
            var passwordHash = _cryptoService.CreateHash(password, salt);

            user.SetPasswordHash(passwordHash, salt);

            user.Events.Add(new NewUserAddedEvent(user));
            await _repository.AddAsync(user, cancellationToken);

            return user;
        }

        public async Task<User> AuthenticationByEmailAndPasswordAsync(string email, string password, CancellationToken cancellationToken)
        {
            Guard.Against.NullOrWhiteSpace(email, nameof(email));
            Guard.Against.NullOrWhiteSpace(password, nameof(password));

            var user = await _repository.GetBySpecAsync(new UserByEmailSpec(email), cancellationToken);
            Guard.Against.NullUser(user);

            var passwordHash = _cryptoService.CreateHash(password, user.PasswordSalt);

            if (passwordHash != user.Password)
            {
                throw new InvalidPasswordException();
            }

            return user;
        }
    }
}
