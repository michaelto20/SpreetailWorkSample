using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.DictionaryService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.AddCommandService
{
    public class AddCommandService : IAddCommandService
    {
        private readonly IDictionaryService<string, HashSet<string>> _dictionaryService;
        public AddCommandService(IDictionaryService<string, HashSet<string>> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public bool ValidateCommand(string[] inputsTokens)
        {
            // must have command, key, and value
            if(inputsTokens == null || inputsTokens.Length != 3)
            {
                return false;
            }

            // first token must be "ADD"
            if (!inputsTokens[0].Trim().ToLower().Equals("add", StringComparison.InvariantCulture))
            {
                return false;
            }

            // validate key
            if (String.IsNullOrWhiteSpace(inputsTokens[1]))
            {
                return false;
            }

            // validate value
            if (String.IsNullOrWhiteSpace(inputsTokens[2]))
            {
                return false;
            }

            return true;
        }

        public bool ExecuteCommand(string[] inputsTokens)
        {
            string key = inputsTokens[1];
            string value = inputsTokens[2];

            var dict = _dictionaryService.GetDict();
            if (dict.ContainsKey(key)){
                if (dict[key].Contains(value))
                {
                    // previously added this key/value pair
                    return false;
                }
                else
                {
                    // Add key/value pair
                    dict[key].Add(value);
                    return true;
                }
            }
            else
            {
                HashSet<string> hs = new HashSet<string>();
                hs.Add(value);
                dict.Add(key, hs);
                return true;
            }
        }

    }
}
