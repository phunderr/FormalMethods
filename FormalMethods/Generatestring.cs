using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Generatestring
    {
        // omzetten naar visualisatie interactie met ui
        HashSet<char> alphabet = new HashSet<char>();
        string[] wordlist;
        int length;
        List<string> strings = new List<string>();

        public Generatestring()
        {
                
        }

        public Generatestring(string vocab,int len)
        {
            for (int i = 0; i < vocab.Length-1; i++) {
               alphabet.Add(vocab[i]);
            }
            length = len;

        }

    }
}
