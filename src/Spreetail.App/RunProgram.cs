using Spreetail.Core.Services.AddCommandService;
using Spreetail.Infrastructure.Services.AddCommandService;
using Spreetail.Core.Services.Command;
using System;
using System.Collections.Generic;
using System.Text;
using Spreetail.Core.Services.HelpCommandService;
using Spreetail.Core.Services.ConsoleService;

namespace Spreetail.App
{
    public class RunProgram
    {
        private readonly IAddCommandService<string, string> _addCommandService;
        private readonly IHelpCommandService _helpCommandService;
        private readonly Dictionary<string, ICommand> _commandMapping;
        private readonly IConsoleService _consoleService;

        public RunProgram(
            IAddCommandService<string, string> addCommandService,
            IHelpCommandService helpCommandService, 
            IConsoleService consoleService)
        {
            _addCommandService = addCommandService;
            _helpCommandService = helpCommandService;
            _consoleService = consoleService;
            
            
            _commandMapping = new Dictionary<string, ICommand>()
            {
                { "add", _addCommandService},
                {"help", _helpCommandService }
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
                    var command = GetCommand(inputTokens[0]);
                    if (command is IOperationCommand<string, string>)
                    {
                        var operationCommand = command as IOperationCommand<string, string>;
                        if (operationCommand.Validate(inputTokens))
                        {
                            operationCommand.Execute(inputTokens[1], inputTokens[2]);
                        }
                    }else if(command is IQueryCommand)
                    {
                        var queryCommand = command as IQueryCommand;
                        if (queryCommand.Validate(inputTokens))
                        {
                            queryCommand.Execute();
                        }
                    }else if(command is IActionCommand<string>)
                    {
                        var actionCommand = command as IActionCommand<string>;
                        if (actionCommand.Validate(inputTokens))
                        {
                            actionCommand.Execute(inputTokens[1]);
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
