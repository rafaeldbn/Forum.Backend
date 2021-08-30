using Forum.Backend.Core.PostAggregate;
using System;
using Xunit;

namespace Forum.Backend.UnitTests.Core.PostAggregate
{
    public class PostConstructor
    {
        private int _testUserId = 1;
        private int _testCategoryId = 1;
        private string _testTitle = "Test title";
        private string _testMessage = "Test message";
        private Post _testPost = null;

        private Post CreatePost()
        {
            return new Post(_testUserId, _testCategoryId, _testTitle, _testMessage);
        }

        [Fact]
        public void InitializesUserId()
        {
            _testPost = CreatePost();

            Assert.Equal(_testUserId, _testPost.UserId);
        }

        [Fact]
        public void InitializesCategoryId()
        {
            _testPost = CreatePost();

            Assert.Equal(_testCategoryId, _testPost.CategoryId);
        }

        [Fact]
        public void InitializesTitle()
        {
            _testPost = CreatePost();

            Assert.Equal(_testTitle, _testPost.Title);
        }

        [Fact]
        public void InitializesMessage()
        {
            _testPost = CreatePost();

            Assert.Equal(_testMessage, _testPost.Message);
        }

        [Fact]
        public void InitializesCreatedAt()
        {
            _testPost = CreatePost();

            Assert.True(_testPost.CreatedAtUtc > DateTime.MinValue && DateTime.UtcNow > _testPost.CreatedAtUtc);
        }

        [Fact]
        public void ThrowsExceptionGivenNoUserId()
        {
            Action action = () => new Post(0, _testCategoryId, _testTitle, _testMessage);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("userId", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNoCategoryId()
        {
            Action action = () => new Post(_testUserId, 0, _testTitle, _testMessage);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("categoryId", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullTitle()
        {
            Action action = () => new Post(_testUserId, _testCategoryId, null, _testMessage);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("title", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenEmptyTitle()
        {
            Action action = () => new Post(_testUserId, _testCategoryId, string.Empty, _testMessage);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("title", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenWhiteSpaceTitle()
        {
            Action action = () => new Post(_testUserId, _testCategoryId, "   ", _testMessage);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("title", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenNullMessage()
        {
            Action action = () => new Post(_testUserId, _testCategoryId, _testTitle, null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("message", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenEmptyMessage()
        {
            Action action = () => new Post(_testUserId, _testCategoryId, _testTitle, string.Empty);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("message", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenWhiteSpaceMessage()
        {
            Action action = () => new Post(_testUserId, _testCategoryId, _testTitle, "    ");

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("message", ex.ParamName);
        }

        [Fact]
        public void UpdateMessage()
        {
            _testPost = CreatePost();
            var newMessage = Guid.NewGuid().ToString();

            _testPost.UpdateMessage(newMessage);

            Assert.Equal(newMessage, _testPost.Message);
            Assert.NotNull(_testPost.UpdatedAtUtc);
        }

        [Fact]
        public void ThrowsExceptionGivenNullUpdateMessage()
        {
            _testPost = CreatePost();

            Action action = () => _testPost.UpdateMessage(null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("message", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenEmptyUpdateMessage()
        {
            _testPost = CreatePost();

            Action action = () => _testPost.UpdateMessage(string.Empty);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("message", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenWhiteSpaceUpdateMessage()
        {
            _testPost = CreatePost();

            Action action = () => _testPost.UpdateMessage("   ");

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("message", ex.ParamName);
        }

        [Fact]
        public void UpdateTitle()
        {
            _testPost = CreatePost();
            var newTitle = Guid.NewGuid().ToString();

            _testPost.UpdateTitle(newTitle);

            Assert.Equal(newTitle, _testPost.Title);
            Assert.NotNull(_testPost.UpdatedAtUtc);
        }

        [Fact]
        public void ThrowsExceptionGivenNullUpdateTitle()
        {
            _testPost = CreatePost();

            Action action = () => _testPost.UpdateTitle(null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("title", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenEmptyUpdateTitle()
        {
            _testPost = CreatePost();

            Action action = () => _testPost.UpdateTitle(string.Empty);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("title", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenWhiteSpaceUpdateTitle()
        {
            _testPost = CreatePost();

            Action action = () => _testPost.UpdateTitle("   ");

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("title", ex.ParamName);
        }
    }
}
