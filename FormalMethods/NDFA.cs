using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class NDFA<T> : Automata<T> where T : IComparable 
    {
        protected NDFA() : base(new SortedSet<char>())
        { }

        protected NDFA(char[] s) : base(new SortedSet<char>(s))
        { }

        protected NDFA(SortedSet<char> symbols) : base(symbols)
        {
        }
    }
}
