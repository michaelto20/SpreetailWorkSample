using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Services.Command
{
    public interface IActionCommand<T> : ICommand   
    {
        bool Validate(string[] inputsTokens);
        bool Execute(T obj);
    }
}
