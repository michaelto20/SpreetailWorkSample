using Spreetail.Core.Services.AllMembersCommandService;
using Spreetail.Core.Services.DictionaryService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.AllMembersCommandService
{
    public class AllMembersCommandService<T, U> : IAllMembersCommandService<T, U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;

        public AllMembersCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// Valid pattern in: command
        /// </summary>
        /// <param name="inputTokens"></param>
        /// <returns></returns>
        public bool Validate(string[] inputTokens)
        {
            bool isValid = true;
            if(inputTokens == null || inputTokens.Length != 1)
            {
                isValid = false;
            }
            else
            {
                isValid = Helpers.Helpers.ValidateCommand(inputTokens[0], "allmembers");
            }

            return isValid;
        }

        public bool Execute()
        {
            bool isValid = true;
            var dict = _dictionaryService.GetDict();
            if(dict.Count == 0)
            {
                Console.WriteLine("(empty set)");
                isValid = false;
            }
            else
            {
                int count = 1;
                foreach(var key in dict.Keys)
                {
                    foreach(var value in dict[key])
                    {
                        Console.WriteLine($"{count}) {value}");
                        count++;
                    }
                }
            }
            return isValid;
        }

    }
}
