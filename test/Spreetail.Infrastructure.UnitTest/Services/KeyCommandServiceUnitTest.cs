using FluentAssertions;
using Moq;
using Spreetail.Core.Services.DictionaryService;
using Spreetail.Infrastructure.Services.KeyCommandService;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Spreetail.Infrastructure.UnitTest.Services
{
    public class KeyCommandServiceUnitTest
    {
        private readonly Mock<IDictionaryService<string, string>> _dictionaryService;

        public KeyCommandServiceUnitTest()
        {
            _dictionaryService = new Mock<IDictionaryService<string, string>>();
        }

        private KeyCommandService<string, string> GetService()
        {
            return new KeyCommandService<string, string>(_dictionaryService.Object);
        }

        [Fact]
        public void KeyCommand_Validate_TooManyTokens_ReturnsFalse()
        {
            // Arrange
            string[] inputTokens = new string[2] { "a", "b" };
            var sut = GetService();

            // Act
            var result = sut.Validate(inputTokens);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void KeyCommand_Validate_NullTokens_ReturnsFalse()
        {
            // Arrange
            string[] inputTokens = null;
            var sut = GetService();

            // Act
            var result = sut.Validate(inputTokens);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("keys", true)]
        [InlineData("Keys", true)]
        [InlineData("", false)]
        [InlineData("k", false)]
        public void KeyCommand_Validate_RangeOfInputs(string a, bool expectedResult)
        {
            // Arrange
            string[] inputTokens = new string[1] { a };
            var sut = GetService();

            // Act
            var result = sut.Validate(inputTokens);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
