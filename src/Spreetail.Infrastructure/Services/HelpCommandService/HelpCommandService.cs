using Spreetail.Core.Services.HelpCommandService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.HelpCommandService
{
    public class HelpCommandService : IHelpCommandService
    {
        public HelpCommandService()
        {

        }

        public bool Validate(string[] inputsTokens)
        {
            bool isValid = true;
            if(inputsTokens == null || inputsTokens.Length != 1)
            {

                isValid = false;
            }
            else
            {
                if(!inputsTokens[0].Trim().ToLower().Equals("help", StringComparison.InvariantCulture))
                {
                    isValid = false;
                }
            }

            if (!isValid)
            {
                Console.WriteLine("Invalid help command, try typing: help");
            }

            return isValid;
        }

        public bool Execute()
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("-----------Help Menu----------");
            Console.WriteLine("------------------------------");
            Console.WriteLine("Valid commands include:");
            Console.WriteLine("ADD <key> <value>");
            Console.WriteLine("KEYS");
            Console.WriteLine("MEMBERS");
            Console.WriteLine("REMOVE <key> <value>");
            Console.WriteLine("REMOVEALL <key>");
            Console.WriteLine("CLEAR");
            Console.WriteLine("KEYEXISTS <key>");
            Console.WriteLine("MEMBEREXISTS <key> <value>");
            Console.WriteLine("ALLMEMBERS");
            Console.WriteLine("ITEMS");
            Console.WriteLine("EXIT");
            Console.WriteLine("------------------------------");
            return true;
        }
    }
}
