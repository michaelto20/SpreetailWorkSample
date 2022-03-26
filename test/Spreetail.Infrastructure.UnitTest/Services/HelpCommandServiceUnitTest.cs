using FluentAssertions;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Infrastructure.Services.HelpCommandService;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Spreetail.Infrastructure.UnitTest.Services
{
    public class HelpCommandServiceUnitTest
    {
        private readonly IHelpCommandService _helpCommandService;
        public HelpCommandServiceUnitTest()
        {
            _helpCommandService = new HelpCommandService();
        }

        [Fact]
        public void HelpCommandService_Validate_ReturnsFalse()
        {
            // Arrange
            string[] inputTokens = new string[2] { "help", "help" };

            // Act
            var result = _helpCommandService.Validate(inputTokens);

            // Assert
            result.Should().BeFalse();
        }

        [Theory]
        [InlineData("hepl", false)]
        [InlineData("", false)]
        [InlineData("help", true)]
        public void HelpCommandService_Validate_VariousTests(string inputString, bool expectedResult)
        {
            // Arrange
            string[] inputTokens = new string[1] { inputString };

            // Act
            var result = _helpCommandService.Validate(inputTokens);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
