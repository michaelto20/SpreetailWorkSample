using Spreetail.Core.Services.DictionaryService;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.DictionaryService
{
    public class DictionaryService<T,U> : IDictionaryService<T,U>
    {
        private Dictionary<T, U> Dict { get; set; } = new Dictionary<T, U>();

        public Dictionary<T, U> GetDict()
        {
            return Dict;
        }
    }
}
