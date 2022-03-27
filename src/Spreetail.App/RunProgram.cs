using Spreetail.Core.Services.AddCommandService;
using Spreetail.Infrastructure.Services.AddCommandService;
using Spreetail.Core.Services.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Core.Services.ConsoleService;
using Spreetail.Core.Services.KeysCommandService;
using Spreetail.Core.Services.MembersCommandService;
using Spreetail.Core.Services.RemoveCommandService;
using Spreetail.Core.Services.RemoveAllCommandService;
using Spreetail.Core.Services.ClearCommandService;
using Spreetail.Core.Services.KeyExistsCommandService;
using Spreetail.Core.Services.MemberExistsCommandService;

namespace Spreetail.App
{
    public class RunProgram<T,U>
    {
        private readonly IAddCommandService<T, U> _addCommandService;
        private readonly IHelpCommandService _helpCommandService;
        private readonly Dictionary<string, ICommand> _commandMapping;
        private readonly IConsoleService _consoleService;
        private readonly IKeyCommandService<T, U> _keyCommandService;
        private readonly IMembersCommandService<T, U> _membersCommandService;
        private readonly IRemoveCommandService<T, U> _removeCommandService;
        private readonly IRemoveAllCommandService<T,U> _removeAllCommandService;
        private readonly IClearCommandService<T, U> _clearCommandService;
        private readonly IKeyExistsCommandService<T, U> _keyExistsCommandService;
        private readonly IMemberExistsCommandService<T, U> _memberExistsCommandService;

        public RunProgram(
            IAddCommandService<T, U> addCommandService,
            IHelpCommandService helpCommandService,
            IConsoleService consoleService, IKeyCommandService<T, U> keyCommandService,
            IMembersCommandService<T, U> membersCommandService,
            IRemoveCommandService<T, U> removeCommandService,
            IRemoveAllCommandService<T, U> removeAllCommandService,
            IClearCommandService<T, U> clearCommandService,
            IKeyExistsCommandService<T, U> keyExistsCommandService, 
            IMemberExistsCommandService<T, U> memberExistsCommandService)
        {
            _addCommandService = addCommandService;
            _helpCommandService = helpCommandService;
            _consoleService = consoleService;
            _keyCommandService = keyCommandService;
            _membersCommandService = membersCommandService;
            _removeCommandService = removeCommandService;
            _removeAllCommandService = removeAllCommandService;
            _clearCommandService = clearCommandService;
            _keyExistsCommandService = keyExistsCommandService;
            _memberExistsCommandService = memberExistsCommandService;


            _commandMapping = new Dictionary<string, ICommand>()
            {
                { "add", _addCommandService},
                {"help", _helpCommandService },
                {"keys", _keyCommandService },
                {"members", _membersCommandService },
                {"remove", _removeCommandService },
                {"removeall", _removeAllCommandService },
                {"clear", _clearCommandService },
                {"keyexists", _keyExistsCommandService },
                {"memberexists", _memberExistsCommandService }
            };
        }

        public void Run() 
        {
            string userInput = "";
            do
            {
                _consoleService.WriteLine("Please enter a command?");
                userInput = _consoleService.ReadLine();
                if (ShouldExit(userInput)) 
                {
                    break;
                }
                var inputTokens = ParserInput(userInput);
                if (inputTokens != null)
                {
                    var command = GetCommand(inputTokens[0].Trim().ToLower());
                    if(command is ICommand)
                    {
                        var queryCommand = command as ICommand;
                        if (queryCommand.Validate(inputTokens))
                        {
                            queryCommand.Execute();
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Could not cast command");
                    }
                    _consoleService.WriteLine("");
                }
            } while (true);

            _consoleService.WriteLine("Goodbye");
        }

        private string[] ParserInput(string userInput)
        {
            if (!String.IsNullOrWhiteSpace(userInput)) 
            {
                return userInput.Split(" ");
            }
            return null;
        }

        private ICommand GetCommand(string command)
        {
            if (_commandMapping.ContainsKey(command))
            {
                return _commandMapping[command];
            }
            else
            {
                return _helpCommandService;
            }
        }

        private bool ShouldExit(string userInput)
        {
            // for a blank input, just repeat the prompt
            if (String.IsNullOrWhiteSpace(userInput))
            {
                return false;
            }

            // only quit the application for the "exit" keyword
            return  userInput.Trim().ToLower().Equals("exit", StringComparison.InvariantCulture);
        }
    }
}
