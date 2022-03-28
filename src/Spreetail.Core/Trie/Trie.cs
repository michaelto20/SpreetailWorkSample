using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Core.Trie
{
    public class Trie : ITrie
    {
        public ITrieNode Root { get; set; }
        public Trie()
        {
            Root = new TrieNode(); 
        }

        
    }
}
