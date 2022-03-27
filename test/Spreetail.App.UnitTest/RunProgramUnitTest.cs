using Moq;
using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.AllMembersCommandService;
using Spreetail.Core.Services.ClearCommandService;
using Spreetail.Core.Services.ConsoleService;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Core.Services.ItemsCommandService;
using Spreetail.Core.Services.KeyExistsCommandService;
using Spreetail.Core.Services.KeysCommandService;
using Spreetail.Core.Services.MemberExistsCommandService;
using Spreetail.Core.Services.MembersCommandService;
using Spreetail.Core.Services.RemoveAllCommandService;
using Spreetail.Core.Services.RemoveCommandService;
using Xunit;

namespace Spreetail.App.UnitTest
{
    public class RunProgramUnitTest
    {
        private readonly Mock<IAddCommandService<string, string>> _mockAddCommandService;
        private readonly Mock<IHelpCommandService> _mockHelpCommandService;
        private readonly Mock<IConsoleService> _mockConsoleService;
        private readonly Mock<IKeyCommandService<string, string>> _mockKeyCommandService;
        private readonly Mock<IMembersCommandService<string, string>> _mockMembersCommandService;
        private readonly Mock<IRemoveCommandService<string, string>> _mockRemoveCommandService;
        private readonly Mock<IRemoveAllCommandService<string, string>> _mockRemoveAllCommandService;
        private readonly Mock<IClearCommandService<string, string>> _mockClearCommandService;
        private readonly Mock<IKeyExistsCommandService<string, string>> _mockKeyExistsCommandService;
        private readonly Mock<IMemberExistsCommandService<string, string>> _mockMemberExistsCommandService;
        private readonly Mock<IAllMembersCommandService<string, string>> _mockAllMembersCommandService;
        private readonly Mock<IItemsCommandService<string, string>> _mockItemsCommandService;

        public RunProgramUnitTest()
        {
            _mockAddCommandService = new Mock<IAddCommandService<string, string>>();
            _mockHelpCommandService = new Mock<IHelpCommandService>();
            _mockConsoleService = new Mock<IConsoleService> ();
            _mockKeyCommandService = new Mock<IKeyCommandService<string, string>>();
            _mockMembersCommandService = new Mock<IMembersCommandService<string, string>>();
            _mockRemoveCommandService = new Mock<IRemoveCommandService<string, string>>();
            _mockRemoveAllCommandService = new Mock<IRemoveAllCommandService<string, string>>();
            _mockClearCommandService = new Mock<IClearCommandService<string, string>>();
            _mockKeyExistsCommandService = new Mock<IKeyExistsCommandService<string, string>>();
            _mockMemberExistsCommandService = new Mock<IMemberExistsCommandService<string, string>>();
            _mockAllMembersCommandService = new Mock<IAllMembersCommandService<string, string>>();
            _mockItemsCommandService = new Mock<IItemsCommandService<string, string>>();
        }

        private RunProgram<string,string> GetProgram<T,U>()
        {
            return new RunProgram<string,string>(
                _mockAddCommandService.Object, 
                _mockHelpCommandService.Object, 
                _mockConsoleService.Object,
                _mockKeyCommandService.Object,
                _mockMembersCommandService.Object,
                _mockRemoveCommandService.Object,
                _mockRemoveAllCommandService.Object,
                _mockClearCommandService.Object,
                _mockKeyExistsCommandService.Object,
                _mockMemberExistsCommandService.Object,
                _mockAllMembersCommandService.Object,
                _mockItemsCommandService.Object);
        }

        [Fact]
        public void RunProgram_Run_CallsHelpCommand_Execut()
        {
            // Arrange
            _mockConsoleService.SetupSequence(x => x.ReadLine()).Returns("help").Returns("exit");
            _mockHelpCommandService.Setup(x => x.Validate(It.IsAny<string[]>())).Returns(true);
            var program = GetProgram<string,string>();

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
            var program = GetProgram<string, string>();

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
            var program = GetProgram<string, string>();

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
            var program = GetProgram<string, string>();

            // Act
            program.Run();

            // Assert
            _mockAddCommandService.Verify(x => x.Execute(), Times.Never);
            _mockHelpCommandService.Verify(x => x.Execute(), Times.Never);
        }
    }
}
