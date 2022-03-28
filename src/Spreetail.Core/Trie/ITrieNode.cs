using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Trie
{
    public interface ITrieNode
    {
        Dictionary<char, ITrieNode> Children { get; set; }
        bool IsWord { get; set; }
    }
}
