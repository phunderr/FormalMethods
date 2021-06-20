﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;


namespace FormalMethods
{
    public class Automata<T> where T : IComparable
    {
        public HashSet<Transition<string>> Transitions { get; set; }

        public SortedSet<State> States { get; private set; }
        public SortedSet<State> StartStates { get; private set; }
        public SortedSet<State> FinalStates { get; private set; }

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
            Transitions = new HashSet<Transition<string>>();
            States = new SortedSet<State>();
            StartStates = new SortedSet<State>();
            FinalStates = new SortedSet<State>();

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

        public void AddTransition(Transition<string> transition)
        {
            Transitions.Add(transition);
            States.Add(transition.from);
            States.Add(transition.to);
        }

        public void AddState(State state)
        {
            States.Add(state);
        }

        public void DefineAsStartState(State state)
        {
            States.Add(state);
            StartStates.Add(state);
        }

        public void DefineAsFinalState(State state)
        {
            States.Add(state);
            FinalStates.Add(state);
        }

        public void PrintTransitions()
        {
            foreach (Transition<string> transition in Transitions)
                Console.WriteLine(transition);
        }

        //public List<T> GetToStates(T from, char symbol)
        //{
        //    List<T> toStates = new List<T>();

        //    foreach (Transition<T> t in Transitions)
        //        if (t.from.Equals(from) && t.symbol.Equals(symbol))
        //            toStates.Add(t.to);

        //    return toStates;
        //}

        public Dictionary<string, List<Transition<string>>> buildgraph()
        {

            IDictionary<string, List<Transition<string>>> map = new Dictionary<string, List<Transition<string>>>();
            foreach (Transition<string> t in Transitions)
            {
                if (map.ContainsKey(t.from.ToString()))
                {
                    List<Transition<string>> trans = map[t.from.ToString()];
                    trans.Add(t);
                    map[t.from.ToString()] = trans;
                }
                else
                {
                    List<Transition<string>> trans = new List<Transition<string>>();
                    trans.Add(t);
                    map.Add(t.from.ToString(), trans);
                }
            }
            return (Dictionary<string, List<Transition<string>>>)map;

        }

        public bool AcceptDFA(string input)
        {
            State currentState;
            bool validpath = false;
            foreach (var start in StartStates)
            {
                currentState = start;
                for (int index = 0; index < input.Length; index++)
                {
                    char curr_symbol = input[index];


                    if (Symbols.Contains(curr_symbol))
                    {

                        List<State> states = new List<State>();

                        // diverse letters dienen nog afgevangen te worden, transition met meerdere zelfde letters
                        foreach (Transition<string> transition in Transitions)
                        {
                            if (transition.from.Equals(currentState) && transition.symbol.Equals(curr_symbol))
                            {
                                currentState = transition.to;
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

        public bool AcceptNDFA(string input)
        {
            State currentState;
            bool validpath = false;
            Dictionary<string, List<Transition<string>>> graph = buildgraph();

            foreach (State start in StartStates)
            {
                currentState = start;

                for (int index = 0; index < input.Length; index++)
                {
                    char curr_symbol = input[index];


                    if (Symbols.Contains(curr_symbol))
                    {
                        if (graph.ContainsKey(currentState.Name))
                        {
                            List<Transition<string>> states = graph[currentState.Name];
                            // diverse letters dienen nog afgevangen te worden, transition met meerdere zelfde letters
                            foreach (Transition<string> transition in states)
                            {


                                if (transition.from.Equals(currentState) && transition.symbol.Equals(curr_symbol))
                                {
                                    currentState = transition.to;
                                    break;
                                }
                                else if (transition.from.Equals(currentState) && transition.symbol.Equals('ε'))
                                {
                                    List<Transition<string>> s1 = graph[transition.to.ToString()];
                                    // diverse letters dienen nog afgevangen te worden, transition met meerdere zelfde letters
                                    foreach (Transition<string> t1 in s1)
                                    {


                                        if (transition.from.Equals(currentState) && transition.symbol.Equals(curr_symbol))
                                        {
                                            currentState = transition.to;
                                            break;
                                        }
                                        else if (transition.from.Equals(currentState) && transition.symbol.Equals('ε'))
                                        {

                                        }
                                    }
                                }
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

        //public List<Transition<string>> Gettransitionpath(char symbol, string state,List<Transition<string>> graph)
        //{
        //    List<Transition<string>> path = new List<Transition<string>>();
        //    foreach()


        //}


        public bool isDFA()
        {
            IDictionary<string, List<Transition<string>>> map = buildgraph();

            foreach (KeyValuePair<string, List<Transition<string>>> s in map)
            {
                List<string> symb = new List<string>();

                foreach (Transition<string> t in s.Value)
                {
                    if (t.symbol != 'ε')
                    {
                        symb.Add(t.symbol.ToString());
                    }
                    else
                    {
                        return false;
                    }

                }
                if (symb.Count != Symbols.Count)
                {
                    return false;
                }
            }
            return true;

        }


        public void reverse()
        {
            SortedSet<State> tempStartStates = StartStates;
            SortedSet<State> tempFinalStates = FinalStates;
            FinalStates = tempStartStates;
            StartStates = tempFinalStates;

            foreach (Transition<string> t in Transitions)
            {
                t.reverse();
            }
        }

        private static List<string> GetCombinations(List<string> elements)
        {
            List<string> combinations = new List<string>();
            combinations.AddRange(elements);
            for (int i = 0; i < elements.Count - 1; i++)
            {
                combinations = (from combination in combinations
                                join element in elements on 1 equals 1
                                let value = string.Join(",", $"{combination}{element}".OrderBy(c => c).Distinct())
                                select value).Distinct().ToList();
            }

            return combinations;
        }

        public Automata<string> toDFA()
        {
            Automata<string> dfa = new Automata<string>(Symbols);
            Dictionary<string, List<Transition<string>>> graph = buildgraph();
            Dictionary<string, List<Transition<string>>> dfagraph = new Dictionary<string, List<Transition<string>>>();

            List<string> liststate = new List<string>();

            foreach (State s in States)
            {
                liststate.Add(s.Name);
            }
            List<string> combinations = GetCombinations(liststate);
            HashSet<string> cleanedstate = new HashSet<string>();
            foreach (string combination in combinations)
            {
                string clean = combination.Trim(new char[] { ' ', ',' });

                cleanedstate.Add(clean);
            }
            foreach (string clean in cleanedstate)
            {
                Debug.WriteLine(clean);

            }
            cleanedstate.Add("{}");

            foreach (string c in cleanedstate)
            {
                dfa.States.Add(new State(c));
            }
            foreach (string c in cleanedstate)
            {
                foreach (State s in FinalStates)
                {
                    if (c.Contains(s.Name))
                    {
                        dfa.FinalStates.Add(new State(c));
                    }
                }

            }

            foreach (State s in StartStates)
            {
                dfa.StartStates.Add(s);
            }





            //foreach (Transition<string> transition in graph) 
            //{

            //}

            return dfa;


        }












    }
}
