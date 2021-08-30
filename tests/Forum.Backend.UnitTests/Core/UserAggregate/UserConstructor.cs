using Forum.Backend.Core.Entities.UserAggregate;
using System;
using Xunit;

namespace Forum.Backend.UnitTests.Core.UserAggregate
{
    public class UserConstructor
    {
        private string _testName = "test name";
        private string _testEmail = "test@test.com";
        private string _testPassword = "123456";
        private string _testTimezone = "E. South America Standard Time";
        private User _testUser = null;

        private User CreateUser()
        {
            return new User(_testName, _testEmail, _testPassword, _testTimezone);
        }

        [Fact]
        public void InitializesName()
        {
            _testUser = CreateUser();

            Assert.Equal(_testName, _testUser.Name);
        }

        [Fact]
        public void InitializesEmail()
        {
            _testUser = CreateUser();

            Assert.Equal(_testEmail, _testUser.Email);
        }

        [Fact]
        public void InitializesPassword()
        {
            _testUser = CreateUser();

            Assert.Equal(_testPassword, _testUser.Password);
        }

        [Fact]
        public void InitializesTimezone()
        {
            _testUser = CreateUser();

            Assert.Equal(_testTimezone, _testUser.TimeZone);
        }

        [Fact]
        public void ThrowsExceptionGivenNullHash()
        {
            _testUser = CreateUser();

            Action action = () => _testUser.SetPasswordHash(null, "123456");

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("passwordHash", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullSalt()
        {
            _testUser = CreateUser();

            Action action = () => _testUser.SetPasswordHash(_testPassword, null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("passwordSalt", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullName()
        {
            Action action = () => new User(null, _testEmail, _testPassword, _testTimezone);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullEmail()
        {
            Action action = () => new User(_testName, null, _testPassword, _testTimezone);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("email", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullPassword()
        {
            Action action = () => new User(_testName, _testEmail, null, _testTimezone);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("password", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullTimezone()
        {
            Action action = () => new User(_testName, _testEmail, _testPassword, null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("timeZone", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullEmailUpdate()
        {
            var user = CreateUser();
            Action action = () => user.UpdateEmail(null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("email", ex.ParamName);
        }

        [Fact]
        public void UpdateEmail()
        {
            var user = CreateUser();
            var newEmail = Guid.NewGuid().ToString();
            user.UpdateEmail(newEmail);

            Assert.Equal(newEmail, user.Email);
        }

        [Fact]
        public void ThrowsExceptionGivenNullNameUpdate()
        {
            var user = CreateUser();
            Action action = () => user.UpdateName(null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void UpdateName()
        {
            var user = CreateUser();
            var newName = Guid.NewGuid().ToString();
            user.UpdateName(newName);

            Assert.Equal(newName, user.Name);
        }
    }
}
