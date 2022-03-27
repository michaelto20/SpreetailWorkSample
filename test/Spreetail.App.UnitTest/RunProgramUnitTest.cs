using Moq;
using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.ConsoleService;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Core.Services.KeysCommandService;
using Xunit;

namespace Spreetail.App.UnitTest
{
    public class RunProgramUnitTest
    {
        private readonly Mock<IAddCommandService<string, string>> _mockAddCommandService;
        private readonly Mock<IHelpCommandService> _mockHelpCommandService;
        private readonly Mock<IConsoleService> _mockConsoleService;
        private readonly Mock<IKeyCommandService<string, string>> _mockKeyCommandService;

        public RunProgramUnitTest()
        {
            _mockAddCommandService = new Mock<IAddCommandService<string, string>>();
            _mockHelpCommandService = new Mock<IHelpCommandService>();
            _mockConsoleService = new Mock<IConsoleService> ();
            _mockKeyCommandService = new Mock<IKeyCommandService<string, string>>();
        }

        private RunProgram GetProgram()
        {
            return new RunProgram(
                _mockAddCommandService.Object, 
                _mockHelpCommandService.Object, 
                _mockConsoleService.Object,
                _mockKeyCommandService.Object);
        }

        [Fact]
        public void RunProgram_Run_CallsHelpCommand_Execut()
        {
            // Arrange
            _mockConsoleService.SetupSequence(x => x.ReadLine()).Returns("help").Returns("exit");
            _mockHelpCommandService.Setup(x => x.Validate(It.IsAny<string[]>())).Returns(true);
            var program = GetProgram();

            // Act
            program.Run();

            // Assert
            _mockHelpCommandService.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void RunProgram_Run_CallsAddCommand_Execute()
        {
            // Arrange
            _mockConsoleService.SetupSequence(x => x.ReadLine()).Returns("add a b").Returns("exit");
            _mockAddCommandService.Setup(x => x.Validate(It.IsAny<string[]>())).Returns(true);
            var program = GetProgram();

            // Act
            program.Run();

            // Assert
            _mockAddCommandService.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void RunProgram_Run_CallsKeysCommand_Execute()
        {
            // Arrange
            _mockConsoleService.SetupSequence(x => x.ReadLine()).Returns("KEYS").Returns("exit");
            _mockKeyCommandService.Setup(x => x.Validate(It.IsAny<string[]>())).Returns(true);
            var program = GetProgram();

            // Act
            program.Run();

            // Assert
            _mockKeyCommandService.Verify(x => x.Execute(), Times.Once);
        }

        [Fact]
        public void RunProgram_Run_Exits()
        {
            // Arrange
            _mockConsoleService.SetupSequence(x => x.ReadLine()).Returns("exit");
            var program = GetProgram();

            // Act
            program.Run();

            // Assert
            _mockAddCommandService.Verify(x => x.Execute(), Times.Never);
            _mockHelpCommandService.Verify(x => x.Execute(), Times.Never);
        }
    }
}
