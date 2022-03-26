using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Services.Command
{
    public interface IOperationCommand<T,U> : ICommand
    {
        bool Validate(string[] inputsTokens);
        bool Execute(T key, U value);
    }
}
