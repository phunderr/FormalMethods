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
            bool letterCapture = false; 
            RegexData regexdata = new RegexData(); 
            foreach (char c in regex.ToCharArray())
            {
                switch (c)
                {
                    case '(':
                        startCapture ++; // caputere group started; 
                        regexdata.newCapture();
                        letterCapture = false; 
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
                        if (letterCapture)
                        {

                        }
                        else
                        {
                            Affector affector = Affector.or;
                            regexdata.fillAffector(affector);
                        }
                        break;
                    case '+': //+ 
                        if (letterCapture)
                        {

                        }
                        else
                        {
                            Affector affector2 = Affector.plus;
                            regexdata.fillAffector(affector2);
                        }
                        break;
                    case '*': //*
                        if (letterCapture)
                        {

                        }
                        else
                        {
                            Affector affector3 = Affector.star;
                            regexdata.fillAffector(affector3);
                        }           
                        break;
                    case '.': //.
                        if (letterCapture)
                        {

                        }
                        else
                        {
                            Affector affector4 = Affector.dot;
                            regexdata.fillAffector(affector4);
                        }   
                        break;
                    default: //no special character detected
                        if (startCapture > 0)
                        {
                            regexdata.addLettertoCapture(c, startCapture);
                            //(ab(cd)*)+
                        }
                        else
                        {
                            if (letterCapture)
                            {
                                regexdata.addLettertoCapture(c, 1);
                            }
                            else
                            {
                                regexdata.newCapture();
                                letterCapture = true;
                                regexdata.addLettertoCapture(c, 1); 
                            }
                            

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
