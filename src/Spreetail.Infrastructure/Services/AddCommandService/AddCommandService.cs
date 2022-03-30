using Spreetail.Core.Services.AddCommandService;
using Spreetail.Core.Services.AutoCompleteService;
using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Trie;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.AddCommandService
{
    public class AddCommandService<T,U> : IAddCommandService<T,U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;
        private readonly ITrieService _trieService;
        public T Key { get; set; }
        public U Value { get; set; }
        public AddCommandService(IDictionaryService<T, U> dictionaryService,
            ITrieService trieService)
        {
            _dictionaryService = dictionaryService;
            _trieService = trieService;
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
                isValid = Helpers.Helpers.ValidateCommand(inputsTokens[0], "add") &&
                    Helpers.Helpers.ValidateKey(inputsTokens[1]) && Helpers.Helpers.ValidateValue(inputsTokens[2]);
            }

            if (!isValid)
            {
                Console.WriteLine(") Invalid ADD command");
            }
            else
            {
                try
                {
                    // make types generic for dictionary
                    Key = Helpers.Helpers.ConvertToGeneric<T>(inputsTokens[1]);
                    Value = Helpers.Helpers.ConvertToGeneric<U>(inputsTokens[2]);

                    // add for autocomplete
                    _trieService.Insert(inputsTokens[1]);
                    _trieService.Insert(inputsTokens[2]);
                }catch(Exception ex)
                {
                    Console.WriteLine(") Invalid key or value");
                    isValid = false;
                }
            }

            return isValid;
        }

        public bool Execute()
        {
            if(AddToDictionary(Key, Value))
            {
                Console.WriteLine(") Added");
                return true;
            }
            else
            {
                Console.WriteLine(") ERROR, member already exists for key");
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
