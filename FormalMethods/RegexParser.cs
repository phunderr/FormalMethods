using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class RegexParser
    {
        public static void ParseRegex(string regex)
        {
            foreach(char c in regex.ToCharArray())
            {
                switch (c)
                {
                    case '(': //ignore
                        break;
                    case ')': //Last

                        break;
                    case '|': //or 

                        break;
                    case '+': //+ 
    
                        break;
                    case '*': //*
 
                        break;
                    case '.': //.
        
                        break;
                    default: //no special character detected
                        break;
                        }
                        break;
                }
            }
        }
    }
}
