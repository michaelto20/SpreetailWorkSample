using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.RemoveCommandService;
using System;
using H = Spreetail.Infrastructure.Helpers;

namespace Spreetail.Infrastructure.Services.RemoveCommandService
{
    public class RemoveCommandService<T,U> : IRemoveCommandService<T,U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;
        public T Key { get; set; }
        public U Value { get; set; }
        public RemoveCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        public bool Validate(string[] inputTokens)
        {
            bool isValid = true;
            if (inputTokens == null || inputTokens.Length != 3)
            {
                isValid = false;
            }
            else
            {
                isValid = H.Helpers.ValidateCommand(inputTokens[0], "remove") && H.Helpers.ValidateKey(inputTokens[1])
                    && H.Helpers.ValidateValue(inputTokens[2]);
            }
            if (isValid)
            {
                try
                {
                    // make types generic for dictionary
                    Key = Helpers.Helpers.ConvertToGeneric<T>(inputTokens[1]);
                    Value = Helpers.Helpers.ConvertToGeneric<U>(inputTokens[2]);
                }catch(Exception ex)
                {
                    Console.WriteLine(") Invalid key or value");
                    isValid = false;
                }
            }
            else
            {
                Console.WriteLine(") Invalid REMOVE command");
            }

            return isValid;
        }

        public bool Execute()
        {
            bool isValid = true;
            var dict = _dictionaryService.GetDict();
            if (dict.ContainsKey(Key) && dict[Key].Contains(Value))
            {
                // remove value
                dict[Key].Remove(Value);

                // if no more values for key, remove key
                if(dict[Key].Count == 0)
                {
                    dict.Remove(Key);
                }
                Console.WriteLine(") Removed");
            }
            else
            {
                isValid = false;
                Console.WriteLine(") ERROR, key does not exist");
            }
            return isValid;
        }
    }
}
