using Forum.Backend.Core.Entities.CategoryAggregate;
using System;
using Xunit;

namespace Forum.Backend.UnitTests.Core.CategoryAggregate
{
    public class CategoryConstructor
    {
        private string _testName = "test name";
        private Category _testCategory = null;

        private Category CreateCategory()
        {
            return new Category(_testName);
        }

        [Fact]
        public void InitializesName()
        {
            _testCategory = CreateCategory();

            Assert.Equal(_testName, _testCategory.Name);
        }

        [Fact]
        public void ThrowsExceptionGivenNullName()
        {
            Action action = () => new Category(null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenEmptyName()
        {
            Action action = () => new Category(string.Empty);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenWhiteSpaceName()
        {
            Action action = () => new Category("        ");

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void UpdateName()
        {
            _testCategory = CreateCategory();

            var newName = Guid.NewGuid().ToString();
            _testCategory.UpdateName(newName);

            Assert.Equal(newName, _testCategory.Name);
        }

        [Fact]
        public void ThrowsExceptionGivenNullUpdateName()
        {
            _testCategory = CreateCategory();

            Action action = () => _testCategory.UpdateName(null);

            var ex = Assert.Throws<ArgumentNullException>(action);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenEmptyUpdateName()
        {
            _testCategory = CreateCategory();

            Action action = () => _testCategory.UpdateName(string.Empty);

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("name", ex.ParamName);
        }

        [Fact]
        public void ThrowsExceptionGivenWhiteSpaceUpdateName()
        {
            _testCategory = CreateCategory();

            Action action = () => _testCategory.UpdateName("        ");

            var ex = Assert.Throws<ArgumentException>(action);
            Assert.Equal("name", ex.ParamName);
        }
    }
}
