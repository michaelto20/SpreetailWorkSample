using Spreetail.Core.Trie;
using System;
using System.Collections.Generic;
using System.Text;

namespace Spreetail.Infrastructure.Services.TrieService
{
    public class TrieService : ITrieService
    {
        public ITrie Trie { get; set; }
        public TrieService()
        {
            Trie = new Trie();
        }

        /// <summary>
        /// Adds each letter of a word to the Trie
        /// </summary>
        /// <param name="word"></param>
        public void Insert(string word)
        {
            var currentNode = Trie.Root;
            foreach (var w in word.ToLower())
            {
                // letter not found, insert
                if (!currentNode.Children.ContainsKey(w))
                {
                    currentNode.Children.Add(w, new TrieNode());
                }
                // move down tree
                currentNode = currentNode.Children[w];
            }
            // word is inserted, so marked that this completes a word
            currentNode.IsWord = true;
        }

        /// <summary>
        /// Returns a collections of words that match the given prefix
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public IEnumerable<string> GetWordsWithMathingPrefix(string prefix)
        {
            ITrieNode prefixNode = FindPrefixNode(prefix.ToLower());
            List<string> prefixMatches = new List<string>();
            if(prefixNode != null)
            {
                FindWordsWithPrefix(prefixNode, prefix, prefixMatches);
            }

            return prefixMatches;
        }


        /// <summary>
        /// Finds node in Trie that matches the given word
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        private ITrieNode FindPrefixNode(string word)
        {
            ITrieNode currentNode = Trie.Root;
            // walk down tree until we find the whole prefix, if it exists
            foreach (char w in word)
            {
                if (currentNode.Children.ContainsKey(w))
                {
                    currentNode = currentNode.Children[w];
                }
                else
                {
                    // prefix doesn't exist in tree
                    return null;
                }
            }
            return currentNode;
        }

        /// <summary>
        /// Uses DFS to find words with a matching prefix
        /// </summary>
        /// <returns></returns>
        private void FindWordsWithPrefix(ITrieNode node, string prefix, List<string> words)
        {
            if (node.IsWord)
            {
                words.Add(prefix);
            }

            // travel down remainder of tree
            foreach((char letter, ITrieNode child) in node.Children)
            {
                FindWordsWithPrefix(child, prefix + letter, words);
            }

        }
    }
}
