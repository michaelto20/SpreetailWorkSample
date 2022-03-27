using Spreetail.Core.Services.ClearCommandService;
using Spreetail.Core.Services.DictionaryService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.ClearCommandService
{
    public class ClearCommandService<T,U> : IClearCommandService<T,U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;

        public ClearCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public bool Validate(string[] inputTokens)
        {
            bool isValid = true;
            if (inputTokens == null || inputTokens.Length != 1)
            {
                isValid = false;
            }
            else
            {
                isValid = Helpers.Helpers.ValidateCommand(inputTokens[0], "clear");
            }

            if (!isValid)
            {
                Console.WriteLine("Invalid CLEAR command");
            }

            return isValid;
        }

        public bool Execute()
        {
            bool isValid = true;
            if(_dictionaryService.GetDict().Count > 0)
            {
                // clear out dictionary by re-instantiating it
                _dictionaryService.Reset();
            }
            Console.WriteLine("Cleared");
            return isValid;
        }
    }
}
