using Forum.Backend.Core.Entities.UserAggregate;
using Forum.Backend.Core.Entities.UserAggregate.Specifications;
using Forum.Backend.Core.Exceptions;
using Forum.Backend.Core.Interfaces;
using Forum.Backend.Core.Services;
using Forum.Backend.Infrastructure.Crypto;
using Forum.Backend.SharedKernel.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Forum.Backend.UnitTests.Core.Services.UserServiceTest
{
    public class AuthenticationByEmailAndPassword
    {
        private string _testName = "test name";
        private string _testEmail = "test@test.com";
        private string _testPassword = "123456";
        private string _testTimezone = "E. South America Standard Time";

        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly ICryptoService _cryptoService;

        public AuthenticationByEmailAndPassword()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _cryptoService = new CryptoService();
        }

        [Fact]
        public async Task ThrowsGivenNullEmail()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.AuthenticationByEmailAndPasswordAsync(null, _testPassword, default));
            Assert.Equal("email", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenNullPassword()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.AuthenticationByEmailAndPasswordAsync(_testEmail, null, default));
            Assert.Equal("password", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenWhitespaceEmail()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AuthenticationByEmailAndPasswordAsync("   ", _testPassword, default));
            Assert.Equal("email", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenWhitespacePassword()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AuthenticationByEmailAndPasswordAsync(_testEmail, "    ", default));
            Assert.Equal("password", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenInvalidUserEmail()
        {
            var userService = new UserService(_mockUserRepository.Object, _cryptoService);

            await Assert.ThrowsAsync<UserNotFoundException>(async () => await userService.AuthenticationByEmailAndPasswordAsync(_testEmail, _testPassword, default));
        }

        [Fact]
        public async Task ThrowsGivenInvalidPassword()
        {
            var userService = new UserService(_mockUserRepository.Object, _cryptoService);
            var testUser = await userService.AddNewUserAsync(_testName, _testEmail, _testPassword, _testTimezone, default);

            _mockUserRepository.Setup(x => x.GetBySpecAsync(It.IsAny<UserByEmailSpec>(), default)).ReturnsAsync(testUser);

            await Assert.ThrowsAsync<InvalidPasswordException>(async () => await userService.AuthenticationByEmailAndPasswordAsync(_testEmail, "123", default));
        }

        [Fact]
        public async Task ShouldAuthenticateAndReturnUser()
        {
            var userService = new UserService(_mockUserRepository.Object, _cryptoService);
            var testUser = await userService.AddNewUserAsync(_testName, _testEmail, _testPassword, _testTimezone, default);

            _mockUserRepository.Setup(x => x.GetBySpecAsync(It.IsAny<UserByEmailSpec>(), default)).ReturnsAsync(testUser);

            var user = await userService.AuthenticationByEmailAndPasswordAsync(_testEmail, _testPassword, default);

            Assert.Equal(user.Id, testUser.Id);
            Assert.Equal(user.Email, testUser.Email);
        }
    }
}
