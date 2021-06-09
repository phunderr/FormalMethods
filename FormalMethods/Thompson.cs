using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Thompson
    {
        private char[] alphabet;
        private Automata<string> automata;
        public Thompson()
        {
            this.alphabet = new char[27];
            automata = new Automata<string>(alphabet);

        }

        public int terminaal(int begin, int list, char a)
        {
            automata.AddTransition(new Transition<string>(begin  + "", a, (list + 1) + ""));
            return list+ 1; 
        }

        public int epsilon(int list1, int list2)
        {
            automata.AddTransition(new Transition<string>(list1 + "", 'ε', list2 + ""));
            return list2; 
        }


        public int dot(int begin, int end, int list, string letters)
        {
           
            //automata.AddTransition(new Transition<string>(begin + "", letters[0], (list + 1) + ""));
           // list++;

            for (int i = 0; i < letters.Length; i++)
            {
                if(i == letters.Length - 1)
                {
                    automata.AddTransition(new Transition<string>(list + "", letters[i], end + ""));
                }
                else
                {
                    automata.AddTransition(new Transition<string>(list + "", letters[i], (list + 1) + ""));

                }
                list++;
            }
            //endEps(end, list);

            return list + 1;
        }

        public int or(int begin, int end, int list, string a)
        {
            beginEps(begin, list); 
            list++;

            
           
            for (int i = 0; i < a.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", a[i], (list + 1) + ""));
                list++;
            }
            
            
            

            
            endEps(end, list); 

            return list + 1; 
        }

        public int plus(int begin,int end, int list, string letters)
        {

            beginEps(begin, list); 
            list++; 
            for(int i =0; i < letters.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", letters[i], (list + 1) + ""));
                list++; 
            }

            epsilon(list + 1, begin);
            endEps(end, list); 
            
            return list + 1;




        }

        public int star(int begin, int end ,int list, string letters)
        {         
            
            beginEps(begin, list); 
            list++;
            int list2 = list; 
            for (int i = 0; i < letters.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", letters[i], (list + 1) + ""));
                list++;
            }
            epsilon(list, list2);
            endEps(end, list); 
            
            if (end <= 0)
            {
                epsilon(begin, list + 1);
            }
            else
            {
                epsilon(begin, end);
            }

            return list + 1;
        }

        public void beginEps(int begin, int list)
        {
            epsilon(begin, list + 1);
        }

        public void endEps(int end, int list)
        {
            if (end <= 0)
            {
                epsilon(list, list + 1);
            }
            else
            {
                epsilon(list, end);
            }
        }

        public void drawThompson()
        {
            Grapher grapher = new Grapher();
            grapher.CreateGraph(automata, "test");
        }

       
    }
}
