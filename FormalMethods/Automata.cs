using System;
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

        public Dictionary<string, List<Transition<string>>> buildreversegraph()
        {

            IDictionary<string, List<Transition<string>>> map = new Dictionary<string, List<Transition<string>>>();
            foreach (Transition<string> t in Transitions)
            {
                if (map.ContainsKey(t.to.ToString()))
                {
                    List<Transition<string>> trans = map[t.to.ToString()];
                    trans.Add(t);
                    map[t.to.ToString()] = trans;
                }
                else
                {
                    List<Transition<string>> trans = new List<Transition<string>>();
                    trans.Add(t);
                    map.Add(t.to.ToString(), trans);
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
                        foreach (Transition<string>transition in Transitions)
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

    



        public bool isDFA() 
        {
            IDictionary<string, List<Transition<string>>> map = buildgraph();

            foreach (KeyValuePair<string,List<Transition<string>>> s in map) 
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
            

            List<string> liststate = new List<string>();
            
            foreach (State s in States)
            {
                liststate.Add(s.Name);
            }
            List<string> combinations = GetCombinations(liststate);
            HashSet<string> cleanedstate = new HashSet<string>();
            foreach (string combination in combinations)
            {
                string clean = combination.Trim(new char[] {' ',','});
               
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


            List<Transition<string>> failedstates = GetfailedStates(graph, cleanedstate);
            foreach (Transition<string> failedt in failedstates) 
            {
                dfa.Transitions.Add(failedt);
            }
            //vind iedere state waar bijv. a naar toegaat. bouw een nieuwe transitie op naar de nieuwe locatie
            List<Transition<string>> correctstate = new List<Transition<string>>();
            foreach (char c in Symbols)
            {
                foreach (State s in dfa.States)
                {
                    List<Transition<string>> statetransitions = new List<Transition<string>>();
                    List<string> words = new List<string>(s.Name.Split(','));
                    foreach (string word in words)
                    {
                        if (graph.ContainsKey(word))
                        {
                            //goodstate bevat collectie van alle transaction van state word
                            List<Transition<string>> goodstate = graph[word];
                            List<string> symbolstates = new List<string>();


                            foreach (Transition<string> transition in goodstate)
                            {
                                if (transition.symbol.Equals(c))
                                {
                                    symbolstates.Add(transition.to.ToString());
                                }

                            }
                            if (symbolstates.Count > 0)
                            {
                                string combstate = Getcombinedstringstates(cleanedstate, symbolstates);
                                symbolstates.Clear();
                                correctstate.Add(new Transition<string>(new State(s.Name), c, new State(combstate)));
                            }

                        }
                    }
                }
            }




            foreach (Transition<string> correct in correctstate)
            {
                dfa.Transitions.Add(correct);
            }


            dfa.RemoveInaccesableStates();

            return dfa;
        }

        public void RemoveInaccesableStates() 
        {
            
            Dictionary<string, List<Transition<string>>> graph = buildreversegraph();
            List<State> removablestates = new List<State>();
            List<Transition<string>> removavletransitions = new List<Transition<string>>();
            foreach (State s in States)
            {
                if (!graph.ContainsKey(s.Name) && !StartStates.Contains(s)) 
                {
                    removablestates.Add(s);
                }
            }

            foreach (State rems in removablestates) 
            {
                foreach (Transition<string> rmt in Transitions) 
                {
                    if (rmt.from.Name == rems.Name) 
                    {
                        removavletransitions.Add(rmt);
                    }
                }
            }

            foreach (State rms in removablestates) 
            {
                States.Remove(rms);
            }

            foreach (Transition<string> rmt in removavletransitions)
            {
                Transitions.Remove(rmt);
            }

        }



        public List<Transition<string>> GetfailedStates(Dictionary<string, List<Transition<string>>> graph, HashSet<string> states) 
        {
            List<Transition<string>> failedtransition = new List<Transition<string>>();

            foreach (string s in states) 
            {
                if (!s.Equals("{}"))
                {
                    List<Transition<string>> statetrans = new List<Transition<string>>();
                    string[] words = s.Split(',');
                    foreach (string word in words)
                    {
                        if (graph.ContainsKey(word))
                            statetrans.AddRange(graph[word]);
                    }



                    SortedSet<char> diff = new SortedSet<char>(Symbols);
                    foreach (Transition<string> trans in statetrans)
                    {
                        if (!trans.symbol.Equals('ε'))
                            diff.Remove(trans.symbol);
                        else 
                        {
                            
                        }
                    }
                    if (diff.Count > 0)
                    {
                        foreach (char c in diff)
                        {
                            failedtransition.Add(new Transition<string>(new State(s), c, new State("{}")));
                        }
                    }
                }
            }
            foreach (char sym in Symbols) 
            {
                failedtransition.Add(new Transition<string>(new State("{}"), sym, new State("{}")));
            }
            return failedtransition;
        }



        public string Getcombinedstringstates(HashSet<string> cleanedstate, List<string> states)
        {
            
            foreach (string rawstate in cleanedstate)
            {
                List<string> words = new List<string>(rawstate.Split(','));

                if (words.Count == states.Count) 
                {
                    if (ContainsAll(words, states)) 
                    {
                        return rawstate;
                    }

                }

            }
            return "{}";

        }

        public static bool ContainsAll<T>(IEnumerable<T> source, IEnumerable<T> values)
        {
            return values.All(value => source.Contains(value));
        }


        public Automata<string> minimaliseren() 
        {
            if (isDFA())
            {
                reverse();
                Automata<string> gen = toDFA();
                gen.reverse();
                Automata<string> gen2 = gen.toDFA();

                return gen2;

            }
            else 
            {
                Automata<string> gen = toDFA();
                gen.reverse();
                Automata<string> gen1 = gen.toDFA();
                gen1.reverse();
                Automata<string> gen2 = gen1.toDFA();

                return gen2;
            }
        }








    }
}
