using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Trie
{
    public interface ITrieService
    {
        void Insert(string word);
        IEnumerable<string> GetWordsWithMathingPrefix(string prefix);
    }
}
