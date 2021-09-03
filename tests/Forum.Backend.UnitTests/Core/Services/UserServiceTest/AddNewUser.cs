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
    public class AddNewUser
    {
        private string _testName = "test name";
        private string _testEmail = "test@test.com";
        private string _testPassword = "123456";
        private string _testTimezone = "E. South America Standard Time";

        private readonly Mock<IRepository<User>> _mockUserRepository;
        private readonly ICryptoService _cryptoService;

        public AddNewUser()
        {
            _mockUserRepository = new Mock<IRepository<User>>();
            _cryptoService = new CryptoService();
        }

        private User CreateUser()
        {
            return new User(_testName, _testEmail, _testPassword, _testTimezone);
        }

        [Fact]
        public async Task ThrowsGivenNullName()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.AddNewUserAsync(null, _testEmail, _testPassword, _testTimezone, default));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenNullEmail()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.AddNewUserAsync(_testName, null, _testPassword, _testTimezone, default));
            Assert.Equal("email", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenNullPassword()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.AddNewUserAsync(_testName, _testEmail, null, _testTimezone, default));
            Assert.Equal("password", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenNullTimezone()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentNullException>(async () => await userService.AddNewUserAsync(_testName, _testEmail, _testPassword, null, default));
            Assert.Equal("timeZone", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenWhitespaceName()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddNewUserAsync("   ", _testEmail, _testPassword, _testTimezone, default));
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenWhitespaceEmail()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddNewUserAsync(_testName, "    ", _testPassword, _testTimezone, default));
            Assert.Equal("email", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenWhitespacePassword()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddNewUserAsync(_testName, _testEmail, "    ", _testTimezone, default));
            Assert.Equal("password", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenWhitespaceTimezone()
        {
            var userService = new UserService(null, null);

            var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await userService.AddNewUserAsync(_testName, _testEmail, _testPassword, "    ", default));
            Assert.Equal("timeZone", ex.ParamName);
        }

        [Fact]
        public async Task ThrowsGivenDuplicateEmail()
        {
            _mockUserRepository.Setup(x => x.GetBySpecAsync(It.IsAny<UserByEmailSpec>(), default)).ReturnsAsync(CreateUser());

            var userService = new UserService(_mockUserRepository.Object, _cryptoService);

            var ex = await Assert.ThrowsAsync<DuplicateEmailException>(async () => await userService.AddNewUserAsync(_testName, _testEmail, _testPassword, _testTimezone, default));
        }

        [Fact]
        public async Task CreatesNewUser()
        {
            var userService = new UserService(_mockUserRepository.Object, _cryptoService);

            var test = await userService.AddNewUserAsync(_testName, _testEmail, _testPassword, _testTimezone, default);

            _mockUserRepository.Verify(x => x.AddAsync(It.Is<User>(x => x.Email == _testEmail), default), Times.Once);
            _mockUserRepository.Verify(x => x.AddAsync(It.Is<User>(x => x.Password != _testPassword), default), Times.Once);
            _mockUserRepository.Verify(x => x.AddAsync(It.Is<User>(x => !string.IsNullOrEmpty(x.PasswordSalt)), default), Times.Once);
        }
    }
}
