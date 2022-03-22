using Spreetail.Core.Services.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.App
{
    public class RunProgram
    {
        private readonly IDataService _dataService;
        public RunProgram(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run() 
        {
            string userInput = "";
            do
            {
                Console.WriteLine("Please enter a command?");
                userInput = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(userInput))
                {

                }
            } while (ShouldExit(userInput));
            
            Console.WriteLine("Goodbye!");
        }

        private bool ShouldExit(string userInput)
        {
            // for a blank input, just repeat the prompt
            if (String.IsNullOrWhiteSpace(userInput))
            {
                return false;
            }

            // only quit the application for the "exit" keyword
            return  !userInput.Trim().ToLower().Equals("exit", StringComparison.InvariantCulture);
        }
    }
}
