using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.DictionaryService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.AddCommandService
{
    public class AddCommandService<T,U> : IAddCommandService<T,U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;
        public AddCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// Valid pattern: command, key, value
        /// </summary>
        /// <param name="inputsTokens"></param>
        /// <returns></returns>
        public bool Validate(string[] inputsTokens)
        {
            // must have command, key, and value
            if(inputsTokens == null || inputsTokens.Length != 3)
            {
                return false;
            }

            return ValidateCommand(inputsTokens[0]) && ValidateKey(inputsTokens[1]) && ValidateValue(inputsTokens[2]);
        }

        private bool ValidateCommand(string command)
        {
            // first token must be "ADD"
            if (String.IsNullOrWhiteSpace(command) || !command.Trim().ToLower().Equals("add", StringComparison.InvariantCulture))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateKey(string key)
        {
            // TODO: validate key's type for extensibility
            if (String.IsNullOrWhiteSpace(key))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool ValidateValue(string value)
        {
            // TODO: validate value's type for extensibility
            if (String.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool Execute(T key, U value)
        {
            if(AddToDictionary(key, value))
            {
                Console.WriteLine("Added");
                return true;
            }
            else
            {
                Console.WriteLine("ERROR, member already exists for key");
                return false;
            }
        }

        private bool AddToDictionary(T key, U value)
        {
            var dict = _dictionaryService.GetDict();
            if (dict.ContainsKey(key))
            {
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
                HashSet<U> hs = new HashSet<U>();
                hs.Add(value);
                dict.Add(key, hs);
                return true;
            }
        }

    }
}
