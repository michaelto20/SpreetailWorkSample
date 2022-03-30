using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.KeyExistsCommandService;
using System;

namespace Spreetail.Infrastructure.Services.KeyExistsCommandService
{
    public class KeyExistsCommandService<T,U> : IKeyExistsCommandService<T,U>
    {
        public T Key { get; set; }
        private readonly IDictionaryService<T, U> _dictionaryService;

        public KeyExistsCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// Valid pattern is: command key
        /// </summary>
        /// <param name="inputTokens"></param>
        /// <returns></returns>
        public bool Validate(string[] inputTokens)
        {
            bool isValid = true;
            if(inputTokens == null || inputTokens.Length != 2)
            {
                isValid = false;
            }
            else
            {
                isValid = Helpers.Helpers.ValidateCommand(inputTokens[0], "keyexists") && Helpers.Helpers.ValidateKey(inputTokens[1]);
            }

            if (isValid)
            {
                // make types generic for dictionary
                try
                {
                    Key = Helpers.Helpers.ConvertToGeneric<T>(inputTokens[1]);
                }catch(Exception ex)
                {
                    Console.WriteLine(") Invalid key");
                    isValid = false;
                }
            }
            else
            {
                Console.WriteLine(") Invalid KEYEXISTS command");
            }
            return isValid;
        }

        public bool Execute()
        {
            bool isValid = true;
            if (_dictionaryService.GetDict().ContainsKey(Key))
            {
                Console.WriteLine(") true");
            }
            else
            {
                Console.WriteLine(") false");
                isValid = false;
            }
            return isValid;
        }
    }
}
