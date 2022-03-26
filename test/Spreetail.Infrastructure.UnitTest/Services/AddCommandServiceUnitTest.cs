using FluentAssertions;
using Moq;
using Spreetail.Core.Services.DictionaryService;
using Spreetail.Infrastructure.Services.AddCommandService;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Spreetail.Infrastructure.UnitTest.Services
{
    public class AddCommandServiceUnitTest
    {
        private readonly Mock<IDictionaryService<string, string>> _mockDictionaryService;
        public AddCommandServiceUnitTest()
        {
            _mockDictionaryService = new Mock<IDictionaryService<string, string>>();
        }

        private AddCommandService<string, string> GetService()
        {
            return new AddCommandService<string,string>(_mockDictionaryService.Object);
        }

        [Theory]
        [InlineData("Add", "a", "b", true)]
        [InlineData("Addd", "a", "b", false)]
        [InlineData("Add", "", "b", false)]
        [InlineData("Add", "", "", false)]
        [InlineData("Add", "a", "", false)]
        public void AddCommandService_ValidateCommand_ReturnsTrue(string command, string key, string value, bool expectedResult)
        {
            // Arrange
            var sut = GetService();
            string[] inputs = new string[3] { command, key, value};

            // Act
            bool result = sut.Validate(inputs);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void AddCommandService_ExecuteCommand_SuccessfullyAdds_ReturnsTrue()
        {
            // Arrange
            var sut = GetService();
            Dictionary<string, HashSet<string>> dict = new Dictionary<string, HashSet<string>>();
            (string command1, string key1, string value1) = ("add", "b", "c");
            (string command2, string key2, string value2) = ("add", "b", "d");
            _mockDictionaryService.Setup(x => x.GetDict()).Returns(dict);

            // Act
            bool result = sut.Execute(key1, value1);
            bool result2 = sut.Execute(key2, value2);

            // Assert
            result.Should().BeTrue();
            dict[key1].Should().Contain(value1);
            dict[key1].Should().Contain(value2);
        }

        [Fact]
        public void AddCommandService_ExecuteCommand_PreviouslyAdded_ReturnsFalse()
        {
            // Arrange
            var sut = GetService();
            Dictionary<string, HashSet<string>> dict = new Dictionary<string, HashSet<string>>();
            (string command1, string key1, string value1) = ("add", "b", "c");
            (string command2, string key2, string value2) = ("add", "b", "c");
            _mockDictionaryService.Setup(x => x.GetDict()).Returns(dict);

            // Act
            bool result = sut.Execute(key1, value2);
            bool result2 = sut.Execute(key2, value2);

            // Assert
            result.Should().BeTrue();
            result2.Should().BeFalse();
            dict[key1].Should().Contain(value2);
        }
    }
}
