using Spreetail.Core.Domain.AddCommand;
using Spreetail.Core.Domain.Command;
using Spreetail.Core.Services.DataService;
using System.Collections.Generic;

namespace Spreetail.Infrastructure.Services.DataService
{
    public class DataService : IDataService
    {
        Dictionary<string, ICommand> CommandMapping = new Dictionary<string, ICommand>() {
            {"Add", new AddCommand() }
            
        };
        public DataService()
        {
        }


        public void ProcessCommand(string inputString)
        {
            // Validate command

            // Execute command
        }
    }
}
