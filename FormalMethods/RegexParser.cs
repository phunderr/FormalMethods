using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class RegexParser
    {
        public void ParseRegex(string regex)
        {
            int startCapture = 0;
            int countList = 0;
            bool affectorClose = false; 
            RegexData regexdata = new RegexData();
            int sorter = 0; 
            foreach (char c in regex.ToCharArray())
            {
                switch (c)
                {
                    case '(':
                        startCapture ++; // caputere group started; 
                        countList++; 
                        regexdata.newCapture();
                        affectorClose = false;
                        
                        break;
                    case ')': //Last
                        regexdata.newRegex(startCapture, sorter); 
                        startCapture--;
                        sorter--;

                        affectorClose = false; 
                        
                        if(startCapture <= 0)
                        {
                            regexdata.CaptureClear(); 
                        }
                        break;
                    case '|': //or 
                        countList++;
                        sorter++;
                        regexdata.newAffecRegex(sorter);
                        regexdata.fillRemainder();

                        affectorClose = true;

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
                        countList++;
                        sorter++;
                        regexdata.newAffecRegex(sorter);
                        regexdata.fillRemainder();

                        affectorClose = true;

                        Affector affector4 = Affector.dot;
                        regexdata.fillAffector(affector4);
                        break;
                    default: //no special character detected
                        if (startCapture > 0)
                        {
                            if (affectorClose)
                            {
                                regexdata.addLettertoRegex(c); 
                            }
                            else
                            {
                                regexdata.addLettertoCapture(c, startCapture);
                            }
                            
                            //(ab(cd)*)+
                        }
                        else
                        {
                            regexdata.newCapture();
                            countList++;
                            regexdata.addLettertoCapture(c, 1);
                            regexdata.newRegex(1, sorter == 0 ? 99999 : sorter);
                            regexdata.CaptureClear();

                            //aabaa
                        }
                        break;
                        
                       
                }
            }
            regexdata.regexList.Sort();
        }
    }
}

// (ab(cd)*)+ aabba; 
// aabbb* 
