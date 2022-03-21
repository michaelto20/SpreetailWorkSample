using Spreetail.Core.Services.DataService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.App
{
    public class RunProgram
    {
        private readonly IDataService _dataService;
        public RunProgram(IDataService dataService)
        {
            _dataService = dataService;
        }

        public void Run() 
        {
            Console.WriteLine("Hello World!");
        }
    }
}
