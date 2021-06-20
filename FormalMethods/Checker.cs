using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Checker
    {
    

        public static bool AND(Automata<string> a1, Automata<string> a2,string tekst) 
        {
            return a1.AcceptDFA(tekst) && a2.AcceptDFA(tekst);
        }

        public  static bool OR(Automata<string> a1, Automata<string> a2, string tekst)
        {
            return a1.AcceptDFA(tekst) || a2.AcceptDFA(tekst);
        }

        public static bool NOT(Automata<string> a1, Automata<string> a2, string tekst)
        {
            return (a1.AcceptDFA(tekst) != a2.AcceptDFA(tekst));
        }



    }


    
    



  
}
