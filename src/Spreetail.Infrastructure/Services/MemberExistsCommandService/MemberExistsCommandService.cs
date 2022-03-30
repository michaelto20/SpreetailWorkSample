using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.MemberExistsCommandService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.MemberExistsCommandService
{
    public class MemberExistsCommandService<T,U> : IMemberExistsCommandService<T,U>
    {
        public T Key { get; set; }
        public U Value { get; set; }
        private readonly IDictionaryService<T, U> _dictionaryService;

        public MemberExistsCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// Valid pattern is: command key value
        /// </summary>
        /// <param name="inputTokens"></param>
        /// <returns></returns>
        public bool Validate(string[] inputTokens)
        {
            bool isValid = true;
            if(inputTokens == null || inputTokens.Length != 3) 
            {
                isValid = false;
            }
            else
            {
                isValid = Helpers.Helpers.ValidateCommand(inputTokens[0], "memberexists") && Helpers.Helpers.ValidateKey(inputTokens[1]);
            }

            if (isValid)
            {
                Key = Helpers.Helpers.ConvertToGeneric<T>(inputTokens[1]);
                Value = Helpers.Helpers.ConvertToGeneric<U>(inputTokens[2]);
            }
            else
            {
                Console.WriteLine(") Invalid MEMBEREXISTS command");
            }
            return isValid;
        }

        public bool Execute()
        {
            bool isValid = true;
            var dict = _dictionaryService.GetDict();
            if(dict.ContainsKey(Key) && dict[Key].Contains(Value))
            {
                Console.WriteLine(") true");
            }
            else
            {
                isValid = false;
                Console.WriteLine(") false");
            }
            return isValid;
        }
    }
}
