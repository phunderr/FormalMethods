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

        public int terminaal(int list, char a)
        {
            automata.AddTransition(new Transition<string>(list + "", a, (list + 1) + ""));
            return list+ 1; 
        }

        public int epsilon(int list1, int list2)
        {
            automata.AddTransition(new Transition<string>(list1 + "", 'ε', list2 + ""));
            return list1+ 1; 
        }


        public int dot(int list, string letters)
        {
          
            for (int i = 0; i < letters.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", letters[i], (list + 1) + ""));
                list++;
            }

            return list; 
        }

        public int or(int list, string a, string b)
        {
            int list2 = list;
            epsilon(list, list + 1); 
            list ++;
           

            for (int i = 0; i < a.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", a[i], (list + 1) + ""));
                list++;
            }
            int endaList = list; 

            epsilon(list2, list + 1);
            list++; 
            for (int i = 0; i < b.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", b[i], (list + 1) + ""));
                list++;
            }

            epsilon(endaList, list + 1);
            epsilon(list, list + 1);

            return list + 1; 
        }

        public int plus(int list, string letters)
        {
            int list2 = list; 
            epsilon(list, list + 1);
            list++; 
            for(int i =0; i < letters.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", letters[i], (list + 1) + ""));
                list++; 
            }
            epsilon(list, list2 + 1);
            epsilon(list, list + 1);

            return list + 1; 

        }

        public int star(int list, string letters)
        {
            int list2 = list;
            epsilon(list, list + 1);
            list++;
            for (int i = 0; i < letters.Length; i++)
            {
                automata.AddTransition(new Transition<string>(list + "", letters[i], (list + 1) + ""));
                list++;
            }
            epsilon(list, list2 + 1);
            epsilon(list, list + 1);
            epsilon(list2, list + 1); 

            return list + 1;
        }

       
    }
}
