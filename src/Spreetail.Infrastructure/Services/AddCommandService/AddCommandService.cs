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
        public T Key { get; set; }
        public U Value { get; set; }
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
            bool isValid = true;

            // must have command, key, and value
            if(inputsTokens == null || inputsTokens.Length != 3)
            {
                isValid = false;
            }
            else
            {
                isValid = ValidateCommand(inputsTokens[0]) && ValidateKey(inputsTokens[1]) && ValidateValue(inputsTokens[2]);
            }

            if (!isValid)
            {
                Console.WriteLine("Invalid add command");
            }
            else
            {
                // make types generic for dictionary
                Key = (T)Convert.ChangeType(inputsTokens[1], typeof(T));
                Value = (U)Convert.ChangeType(inputsTokens[2], typeof(U));
            }

            return isValid;
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

        public bool Execute()
        {
            if(AddToDictionary(Key, Value))
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
