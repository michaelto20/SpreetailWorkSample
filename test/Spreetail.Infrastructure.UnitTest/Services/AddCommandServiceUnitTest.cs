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
        private readonly Mock<IDictionaryService<string, HashSet<string>>> _mockDictionaryService;
        public AddCommandServiceUnitTest()
        {
            _mockDictionaryService = new Mock<IDictionaryService<string, HashSet<string>>>();
        }

        private AddCommandService GetService()
        {
            return new AddCommandService(_mockDictionaryService.Object);
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
            bool result = sut.ValidateCommand(inputs);

            // Assert
            result.Should().Be(expectedResult);
        }

        [Fact]
        public void AddCommandService_ExecuteCommand_SuccessfullyAdds_ReturnsTrue()
        {
            // Arrange
            var sut = GetService();
            Dictionary<string, HashSet<string>> dict = new Dictionary<string, HashSet<string>>();
            string[] inputs = new string[3] { "a", "b", "c" };
            string[] inputs2 = new string[3] { "a", "b", "d" };
            _mockDictionaryService.Setup(x => x.GetDict()).Returns(dict);

            // Act
            bool result = sut.ExecuteCommand(inputs);
            bool result2 = sut.ExecuteCommand(inputs2);

            // Assert
            result.Should().BeTrue();
            dict[inputs[1]].Should().Contain(inputs[2]);
            dict[inputs[1]].Should().Contain(inputs2[2]);
        }

        [Fact]
        public void AddCommandService_ExecuteCommand_PreviouslyAdded_ReturnsFalse()
        {
            // Arrange
            var sut = GetService();
            Dictionary<string, HashSet<string>> dict = new Dictionary<string, HashSet<string>>();
            string[] inputs = new string[3] { "a", "b", "c" };
            string[] inputs2 = new string[3] { "a", "b", "c" };
            _mockDictionaryService.Setup(x => x.GetDict()).Returns(dict);

            // Act
            bool result = sut.ExecuteCommand(inputs);
            bool result2 = sut.ExecuteCommand(inputs2);

            // Assert
            result.Should().BeTrue();
            result2.Should().BeFalse();
            dict[inputs[1]].Should().Contain(inputs[2]);
        }
    }
}
