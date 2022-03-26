using Spreetail.Core.Services.ConsoleService;
using System;

namespace Spreetail.Infrastructure.Domain.ConsoleIO
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine(string s)
        {
            Console.WriteLine(s);
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }
    }
}
