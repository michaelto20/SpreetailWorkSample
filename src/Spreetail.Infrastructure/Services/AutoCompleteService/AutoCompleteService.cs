using Spreetail.Core.Services.AutoCompleteService;
using Spreetail.Core.Trie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spreetail.Infrastructure.Services.AutoCompleteService
{
    public class AutoCompleteService : IAutoCompleteService
    {
        private readonly ITrieService _trieService;
        public AutoCompleteService(ITrieService trieService)
        {
            _trieService = trieService;
        }
        public string HandleUserInput()
        {
            bool inputChanged = true;
            List<string> candidateWords = new List<string>();
            int candidateCount = 0;
            StringBuilder sb = new StringBuilder();
            List<ConsoleKey> keysToIgnore = new List<ConsoleKey>()
            {
                ConsoleKey.Escape,
                ConsoleKey.DownArrow,
                ConsoleKey.UpArrow
            };

            // intercept key info
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            // if key is enter, return user input
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                // autocomplete
                if (keyInfo.Key == ConsoleKey.Tab)
                {
                    // only re-query Trie is user has entered a new character
                    if (inputChanged)
                    {
                        inputChanged = false;
                        candidateWords = _trieService.GetWordsWithMathingPrefix(sb.ToString()).ToList();
                        candidateCount = 0;
                    }

                    if (candidateWords != null && candidateWords.Any())
                    {
                        if (candidateCount >= candidateWords.Count)
                        {
                            // reset autocomplete loop
                            candidateCount = 0;
                        }

                        ClearCurrentConsoleLine();
                        // display candidate match
                        Console.Write(candidateWords[candidateCount]);
                        sb.Clear();
                        sb.Append(candidateWords[candidateCount]);
                        candidateCount++;
                    }
                    keyInfo = Console.ReadKey(true);
                }
                else
                {
                    // handle backspace
                    if (keyInfo.Key == ConsoleKey.Backspace && sb.Length > 0)
                    {
                        // remove last character
                        sb.Remove(sb.Length - 1, 1);
                        ClearCurrentConsoleLine();
                        Console.Write(sb.ToString());
                    }
                    else
                    {
                        // TODO: Add validation, many keys we don't want to act on
                        sb.Append(keyInfo.KeyChar);
                        Console.Write(keyInfo.KeyChar);
                    }
                    
                    inputChanged = true;
                    keyInfo = Console.ReadKey(true);
                }
            }
            Console.WriteLine();
            return sb.ToString();
        }

        /// <summary>
        /// Clear line
        /// https://stackoverflow.com/a/8946847/1188513
        /// </summary>
        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }
    }
}
