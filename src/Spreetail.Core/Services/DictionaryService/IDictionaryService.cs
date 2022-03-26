using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Services.DictionaryService
{
    public interface IDictionaryService<T,U>
    {
        Dictionary<T, HashSet<U>> GetDict();
    }
}
