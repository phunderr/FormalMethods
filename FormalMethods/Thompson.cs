using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Thompson
    {
        
        private Automata<string> automata;
        public int startState;
        public int finalState; 
        public Thompson(char[] alphabet)
        {
            
            automata = new Automata<string>(alphabet);
            this.startState = 0;
            this.finalState = 0; 
        }
        
        public int terminaal(int begin, int list, char a)
        {
            if(startState == 0)
            {
                this.startState = begin; 
            }
            automata.AddTransition(new Transition<string>(new State(begin  + ""), a, new State((list + 1) + "")));
            this.finalState = list + 1; 
            return list+ 1; 
        }

        public int epsilon(int list1, int list2)
        {
            if (startState == 0)
            {
                this.startState = list1;
            }
            automata.AddTransition(new Transition<string>(new State(list1 + ""), 'ε', new State( list2 + "")));
            this.finalState = list2; 
            return list2; 
        }




        public int or(int begin, int end, int list)
        {
            list = beginEps(begin, list);
            
                
            list = endEps(list + 1, end); 

            return list; 
        }

        public int plus(int begin,int end, int list)
        {

            list = beginEps(begin, list);
            int list2 = list;

            list = endEps(list + 1, end);


            if (end <= 0)
            {
                epsilon(list - 1, list2);
            }
            else
            {
                epsilon(list, list2);
            }


            return list;

        }

        public int star(int begin, int end ,int list)
        {

            list = beginEps(begin, list);
            int list2 = list;

            list = endEps(list + 1, end);

            
            if (end <= 0)
            {
                epsilon(begin, list);
                epsilon(list - 1, list2);
            }
            else
            {
                epsilon(begin, end);
                epsilon(list, list2);
            }


            return list;
        }

        public int beginEps(int begin, int list)
        {
            list++; 
            epsilon(begin, list);
            return list;
        }

        public int endEps(int list, int end)
        {
            if (end <= 0)
            {
                list++; 
                epsilon(list - 1, list);
                return list; 
            }
            else
            {
                epsilon(list, end);
                return list; 
            }
        }

        public void drawThompson()
        {
            Grapher grapher = new Grapher();
            grapher.CreateGraph(automata, "test");
            //give automata
        }

        public void finishThompson()
        {
            automata.DefineAsStartState(this.startState + "");
            automata.DefineAsFinalState(this.finalState + ""); 

        }
        public Automata<string> GetAutomata()
        {
            return this.automata; 
        }

       
    }
}
