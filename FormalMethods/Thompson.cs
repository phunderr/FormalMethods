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
        }

       
    }
}
