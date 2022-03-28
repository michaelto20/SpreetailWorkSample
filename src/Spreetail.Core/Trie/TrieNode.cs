using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Trie
{
    public class TrieNode : ITrieNode
    {
        public Dictionary<char, ITrieNode> Children { get; set; }
        public bool IsWord { get; set; }

        public TrieNode()
        {
            Children = new Dictionary<char, ITrieNode>();
        }
    }
}
