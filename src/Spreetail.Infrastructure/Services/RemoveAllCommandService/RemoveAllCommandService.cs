using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.RemoveAllCommandService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.RemoveAllCommandService
{
    public class RemoveAllCommandService<T,U> : IRemoveAllCommandService<T,U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;
        public T Key { get; set; }

        public RemoveAllCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public bool Validate(string[] inputTokens)
        {
            bool isValid = true;
            if (inputTokens == null || inputTokens.Length != 2)
            {
                isValid = false;
            }
            else
            {
                isValid = Helpers.Helpers.ValidateCommand(inputTokens[0], "removeall") && Helpers.Helpers.ValidateKey(inputTokens[1]);
            }

            if (isValid)
            {
                // make types generic for dictionary
                Key = (T)Convert.ChangeType(inputTokens[1], typeof(T));
            }
            else
            {
                Console.WriteLine(") Invalid REMOVEALL command");
            }

            return isValid;
        }

        public bool Execute()
        {
            bool isValid = true;
            var dict = _dictionaryService.GetDict();
            if (dict.ContainsKey(Key))
            {
                // remove key from dictionary
                dict.Remove(Key);
                Console.WriteLine(") Removed");
            }
            else
            {
                isValid = false;
                Console.WriteLine(") ERROR, key does not exit");
            }
            return isValid;
        }
    }
}
