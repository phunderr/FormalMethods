using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class RegexParser
    {
        public void ParseRegex(string regex)
        {
           
            int idCounter = 0;
            int currentId = 0; 
           
            RegexData regexdata = new RegexData();

            //aa(bb)+aa
            //aa(1+aa
            //bb
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
                        break;
                    case ')': //Last
                        currentId = regexdata.getTopLayer(currentId); 
                        break;                   
                    default: //no special character detected
                        regexdata.addLetter(currentId, c + "");
                        break;
                        
                       
                }
            }
            //regexdata.regexList.Sort();
            regexdata.startThompson(); 
        }
    }
}

// (ab(cd)*)+ aabba; 
// aabbb* 
