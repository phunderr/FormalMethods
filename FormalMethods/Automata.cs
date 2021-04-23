using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    public class Automata<T> where T : IComparable
    {
        public HashSet<Transition<T>> Transitions { get; private set; }

        public SortedSet<T> States { get; private set; }
        public SortedSet<T> StartStates { get; private set; }
        public SortedSet<T> FinalStates { get; private set; }

        public SortedSet<char> Symbols { get; private set; }

        protected Automata() : this(new SortedSet<char>())
        { }

        protected Automata(char[] s) : this(new SortedSet<char>(s))
        { }

        protected Automata(SortedSet<char> symbols)
        {
            Transitions = new HashSet<Transition<T>>();
            States = new SortedSet<T>();
            StartStates = new SortedSet<T>();
            FinalStates = new SortedSet<T>();

            SetAlphabet(symbols);
        }

        public void SetAlphabet(char[] symbols)
        {
            Symbols = new SortedSet<char>(symbols);
        }

        public void SetAlphabet(SortedSet<char> symbols)
        {
            Symbols = symbols;
        }

        public void AddTransition(Transition<T> transition)
        {
            Transitions.Add(transition);
            States.Add(transition.FromState);
            States.Add(transition.ToState);
        }

        public void DefineAsStartState(T state)
        {
            States.Add(state);
            StartStates.Add(state);
        }

        public void DefineAsFinalState(T state)
        {
            States.Add(state);
            FinalStates.Add(state);
        }

        public void PrintTransitions()
        {
            foreach (Transition<T> transition in Transitions)
                Console.WriteLine(transition);
        }

        

        public List<T> GetToStates(T from, char symbol)
        {
            List<T> toStates = new List<T>();

            foreach (Transition<T> t in Transitions)
                if (t.FromState.Equals(from) && t.Symbol.Equals(symbol))
                    toStates.Add(t.ToState);

            return toStates;
        }

        
    }
}
