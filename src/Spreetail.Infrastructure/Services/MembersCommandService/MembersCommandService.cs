using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.MembersCommandService;
using System;
using H = Spreetail.Infrastructure.Helpers;

namespace Spreetail.Infrastructure.Services.MembersCommandService
{
    public class MembersCommandService<T,U> : IMembersCommandService<T,U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;

        public T Key { get; set; }
        public MembersCommandService(IDictionaryService<T, U> dictionaryService)
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
                isValid = H.Helpers.ValidateCommand(inputTokens[0], "members") && H.Helpers.ValidateKey(inputTokens[1]);
            }
            if (isValid)
            {
                // make types generic for dictionary
                Key = (T)Convert.ChangeType(inputTokens[1], typeof(T));
            }
            else
            {
                Console.WriteLine(") Invalid MEMBERS command");
            }

            return isValid;
        }
                
        public bool Execute()
        {
            bool isValid = true;
            var dict = _dictionaryService.GetDict();
            if (dict.ContainsKey(Key))
            {
                int count = 1;
                foreach(var k in dict[Key])
                {
                    Console.WriteLine($"{count}) {k}");
                    count++;
                }
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
