using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class RegexParser
    {
        public RegexData regexdata; 


        public bool ParseRegex(string regex)
        {
           
            int idCounter = 0;
            int currentId = 0;
            string affectors = "+*|.";
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[a-z0-9|()+.*]+$");
            if (!reg.IsMatch(regex))
            {
                return false;
            }
            

            int Ocounter = 0;
            int Ccounter = 0; 
            
           
            this.regexdata = new RegexData();
            regexdata.newRegex(idCounter, "", currentId);
            foreach (char c in regex.ToCharArray())
            {
                switch (c)
                {
                    case '(':
                        idCounter++; 
                        regexdata.newRegex(idCounter, "", currentId);
                        regexdata.addLetter(currentId,  idCounter + "(");
                        currentId = idCounter;
                        Ocounter++; 
                        break;
                    case ')': //Last
                        currentId = regexdata.getTopLayer(currentId);
                        Ccounter++;
                        break;                   
                    default: //no special character detected
                        regexdata.addLetter(currentId, c + "");
                        if (!regexdata.Alphabet.Contains(c) && !affectors.Contains(c))
                        {
                            regexdata.addToAlphabet(c); 
                        }
                        break;
                        
                       
                }
            }
            if(Ocounter != Ccounter)
            {
                return false; //bad regex
            }
           
            regexdata.startThompson();
            return true; 
        }

        public Automata<string> GetAutomata()
        {
            return this.regexdata.GetAutomata();
        }
    }
}


