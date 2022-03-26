using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Services.Command
{
    public interface IQueryCommand : ICommand
    {
        bool Validate(string[] inputsTokens);
        bool Execute();
    }
}
