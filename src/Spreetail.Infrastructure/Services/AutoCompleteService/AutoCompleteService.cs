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
        private bool InputChanged { get; set; }
        private StringBuilder UserInput = new StringBuilder();
        private int CandidateCount = 0;
        private string[] Tokens = null;
        private List<string> CandidateWords = new List<string>();


        public AutoCompleteService(ITrieService trieService)
        {
            _trieService = trieService;
        }
        public string HandleUserInput()
        {
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
                // autocomplete triggered
                if (keyInfo.Key == ConsoleKey.Tab)
                {
                    HandleTab();
                    keyInfo = Console.ReadKey(true);
                }
                else
                {
                    // handle backspace
                    if (keyInfo.Key == ConsoleKey.Backspace && UserInput.Length > 0)
                    {
                        HandleBackspace();
                    }
                    else
                    {
                        // Limit keys to letters, numbers and special characters
                        if ((int)keyInfo.KeyChar > 31 && (int)keyInfo.KeyChar < 177)
                        {
                            UserInput.Append(keyInfo.KeyChar);
                            Console.Write(keyInfo.KeyChar);
                        }
                    }

                    InputChanged = true;
                    keyInfo = Console.ReadKey(true);
                }
            }
            Console.WriteLine();
            string result =  UserInput.ToString();
            ResetService();
            return result;
        }

        private void ResetService()
        {
            InputChanged = true;
            CandidateCount = 0;
            CandidateWords = new List<string>();
            UserInput = new StringBuilder();
            Tokens = null;
        }

        private void HandleBackspace()
        {
            // remove last character
            UserInput.Remove(UserInput.Length - 1, 1);
            ClearCurrentConsoleLine();
            Console.Write(UserInput.ToString());
        }

        private void HandleTab()
        {
            // only re-query Trie is user has entered a new character
            if (InputChanged)
            {
                InputChanged = false;
                // get most recent word for search
                Tokens = UserInput.ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                CandidateWords = _trieService.GetWordsWithMathingPrefix(Tokens[Tokens.Length - 1]).ToList();
                CandidateWords.Sort();
                CandidateCount = 0;
            }

            if (CandidateWords != null && CandidateWords.Any())
            {
                Tokens = UserInput.ToString().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                if (CandidateCount >= CandidateWords.Count)
                {
                    // reset autocomplete loop
                    CandidateCount = 0;
                }

                ClearCurrentConsoleLine();
                
                // update user input with prefix match
                UserInput.Remove(UserInput.Length - Tokens[Tokens.Length - 1].Length, Tokens[Tokens.Length - 1].Length);
                UserInput.Append(CandidateWords[CandidateCount]);
                
                // display candidate match
                Console.Write(UserInput.ToString());
                CandidateCount++;
            }
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
