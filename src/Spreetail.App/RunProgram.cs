using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.DataService;
using Spreetail.Infrastructure.Services.AddCommandService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.App
{
    public class RunProgram
    {
        private readonly IAddCommandService<string, string> _addCommandService;

        private readonly Dictionary<string, IDataService<string, string>> _serviceMapping;
        
        public RunProgram(IAddCommandService<string, string> addCommandService)
        {
            _addCommandService = addCommandService;
            _serviceMapping = new Dictionary<string, IDataService<string, string>>()
            {
                { "add", _addCommandService}
            };
        }

        public void Run() 
        {
            string userInput = "";
            do
            {
                Console.WriteLine("Please enter a command?");
                userInput = Console.ReadLine();
                if (ShouldExit(userInput)) 
                {
                    break;
                }
                var inputTokens = ParserInput(userInput);
                if (inputTokens != null)
                {
                    var dataService = GetCommandService(inputTokens[0]);
                    if (dataService.Validate(inputTokens))
                    {
                        dataService.Execute(inputTokens[1], inputTokens[2]);
                    }
                }
            } while (true);
            
            Console.WriteLine("Goodbye!");
        }

        private string[] ParserInput(string userInput)
        {
            if (!String.IsNullOrWhiteSpace(userInput)) 
            {
                return userInput.Split(" ");
            }
            return null;
        }

        private IDataService<string, string> GetCommandService(string command)
        {
            if (_serviceMapping.ContainsKey(command))
            {
                return _serviceMapping[command];
            }
            else
            {
                // TODO: return help service
                throw new NotImplementedException();
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
