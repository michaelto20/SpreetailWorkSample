using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Helpers
{
    public static class Helpers
    {
        public static bool ValidateCommand(string command, string expectedCommand)
        {
            if (String.IsNullOrWhiteSpace(command))
            {
                return false;
            }

            return command.Trim().ToLower().Equals(expectedCommand, StringComparison.InvariantCulture);
        }

        public static bool ValidateKey(string key)
        {
            if (String.IsNullOrWhiteSpace(key))
            {
                return false;
            }

            return true;
        }
    }
}
