using Spreetail.Core.Domain.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Domain.AddCommand
{
    public class AddCommand : IAddCommand, ICommand
    {
        public string Key { get; } = "ADD";

    }
}
