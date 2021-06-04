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
           // automata = new Automata<string>(alphabet);

        }

        public void terminaal(int list, char a)
        {
            automata.AddTransition(new Transition<string>(list + "", a, (list + 1) + ""));
            list += 2; 
        }

        public void epsilon(int list)
        {
            automata.AddTransition(new Transition<string>(list + "", 'ε', (list + 1) + ""));
            list += 2;
        }


        public void or()
        {
            automata.AddTransition(new Transition<string>("0", 'a', "1"));
            automata.AddTransition(new Transition<string>("0", 'b', "4"));

         


        }

        public void plus()
        {

        }

        public void star()
        {

        }

        public void dot()
        {

        }
    }
}
