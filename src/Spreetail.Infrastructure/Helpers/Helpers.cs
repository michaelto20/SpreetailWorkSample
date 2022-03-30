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

        public static bool ValidateValue(string value)
        {
            // TODO: validate value's type for extensibility
            if (String.IsNullOrWhiteSpace(value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Make types generic for dictionary
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="convertMe"></param>
        /// <returns></returns>
        public static T ConvertToGeneric<T>(string convertMe)
        {
            
            return (T)Convert.ChangeType(convertMe, typeof(T));
        }
    }
}
