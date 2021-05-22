using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class RegexParser
    {
        public static void ParseRegex(string regex)
        {
            int startCapture = 0;
            bool hasLast = false; 
            RegexData regexdata = new RegexData(); 
            foreach (char c in regex.ToCharArray())
            {
                switch (c)
                {
                    case '(':
                        startCapture ++; // caputere group started; 
                        regexdata.newCapture();
                        break;
                    case ')': //Last
                        regexdata.newRegex(startCapture); 
                        startCapture--;
                        if(startCapture <= 0)
                        {
                            regexdata.CaptureClear(); 
                        }
                        break;
                    case '|': //or 
                        Affector affector = Affector.or;      
                        regexdata.fillAffector(affector); 
                        break;
                    case '+': //+ 
                        Affector affector2 = Affector.plus;
                        regexdata.fillAffector(affector2);
                        break;
                    case '*': //*
                        Affector affector3 = Affector.star;
                        regexdata.fillAffector(affector3);
                        break;
                    case '.': //.
                        Affector affector4 = Affector.dot;
                        regexdata.fillAffector(affector4);
                        break;
                    default: //no special character detected
                        if (startCapture > 0)
                        {
                            regexdata.addLettertoCapture(c, startCapture);
                            //(ab(cd)*)+
                        }
                        else
                        {

                            //aabaa
                        }
                        break;
                       
                }
            }
        }
    }
}

// (ab(cd)*)+ aabba; 
// aabbb* 
