using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;

namespace FormalMethods
{
    public class Automata<T> where T : IComparable
    {
        public HashSet<Transition<T>> Transitions { get; set; }

        public SortedSet<T> States { get; private set; }
        public SortedSet<T> StartStates { get; private set; }
        public SortedSet<T> FinalStates { get; private set; }

        public SortedSet<char> Symbols { get; private set; }

        protected Automata() : this(new SortedSet<char>())
        { }

        public Automata(char[] s) : this(new SortedSet<char>(s))
        {
            foreach (char c in s)
            {
                Symbols.Add(c);
            }


        }

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

        public bool Accept(string input)
        {
            T currentState;
            bool validpath = false;
            foreach (var start in StartStates) 
            {
                currentState = start;
                for (int index = 0; index < input.Length; index++)
                {
                    char curr_symbol = input[index];
                    

                    if (Symbols.Contains(curr_symbol))
                    {
                                                   // diverse letters dienen nog afgevangen te worden, transition met meerdere zelfde letters
                            foreach (Transition<T> transition in Transitions) 
                            {
                                if (transition.FromState.Equals(currentState) && transition.Symbol.Equals(curr_symbol))
                                {
                                    currentState = transition.ToState;
                                break;
                                }
                            }

                        
                    }
                    else
                    {
                        Debug.WriteLine(curr_symbol + "is not in alphabet defined");
                    }
                }
                if (FinalStates.Contains(currentState)) 
                {
                    validpath = true;
                }
            }
            Debug.WriteLine(input + validpath);
            return validpath;


        }



  



    }
}
