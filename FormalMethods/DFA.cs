using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class DFA<T> : Automata<T> where T : IComparable
    {
        protected DFA() : base(new SortedSet<char>())
        { }

        protected DFA(char[] s) : base(new SortedSet<char>(s))
        { }

        protected DFA(SortedSet<char> symbols) : base(symbols)
        {
        }

        private bool IsDFA()
        {
            bool isDFA = true;

            foreach (T from in States)
                foreach (char symbol in Symbols)
                {
                    isDFA = isDFA && GetToStates(from, symbol).Count == 1;
                    if (!isDFA)
                        return false;
                }

            return true;
        }

        /// <summary>
        /// Checks if input is valid, always returns false if ndfa
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool AcceptInputDfa(string input)
        {
            if (!IsDFA())
                return false;

            char[] inputSymbols = input.ToCharArray();

            foreach (char symbol in inputSymbols)
                if (!Symbols.Contains(symbol))
                    return false;

            List<Transition<T>> startStatesTransitions = new List<Transition<T>>();
            foreach (Transition<T> t in Transitions)
                if (StartStates.Contains(t.FromState))
                    startStatesTransitions.Add(t);

            T nextState1Temp = default(T);
            foreach (Transition<T> state in startStatesTransitions)
            {
                if (state.Symbol == inputSymbols[0])
                {
                    nextState1Temp = state.ToState;
                    break;
                }
            }

            List<Transition<T>> nextState1 = new List<Transition<T>>();
            foreach (Transition<T> t in Transitions)
                if (t.FromState.Equals(nextState1Temp))
                    nextState1.Add(t);

            return true;
        }

    }
}
