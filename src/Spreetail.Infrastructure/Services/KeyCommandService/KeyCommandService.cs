﻿using Spreetail.Core.Services.DictionaryService;
using Spreetail.Core.Services.KeysCommandService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spreetail.Infrastructure.Services.KeyCommandService
{
    public class KeyCommandService<T,U> : IKeyCommandService<T,U>
    {
        private readonly IDictionaryService<T, U> _dictionaryService;

        public KeyCommandService(IDictionaryService<T, U> dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        /// <summary>
        /// Validates that the Key command takes no parameters and is spelled correctly
        /// </summary>
        /// <param name="inputsTokens"></param>
        /// <returns></returns>
        public bool Validate(string[] inputsTokens)
        {
            bool isValid = true;
            if(inputsTokens == null || inputsTokens.Length != 1)
            {
                isValid = false;
            }
            else
            {
                if(!inputsTokens[0].Trim().ToLower().Equals("keys", StringComparison.InvariantCulture))
                {
                    isValid = false;
                }
            }


            if (!isValid)
            {
                Console.WriteLine(") Invalid KEY command, try typing: HELP");
            }

            return isValid;
        }

        /// <summary>
        /// Returns all the keys in the dictionary
        /// </summary>
        /// <returns></returns>
        public bool Execute() 
        {
            var keys = _dictionaryService.GetDict().Keys.ToList();
            if (keys.Count == 0)
            {
                Console.WriteLine("(empty set)");
                return false;
            }
            else
            {
                for (int i = 0; i < keys.Count; i++)
                {
                    Console.WriteLine($"{i+1}) {keys[i]}");
                }
                return true;
            }

        }
            
    }
}
