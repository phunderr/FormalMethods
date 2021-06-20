using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    
    class RegexData
    {
       
        public List <Regex> regexList;
        public string Alphabet;
        public Thompson Thompson; 
        public RegexData()
        {
            this.regexList = new List<Regex>();
            this.Alphabet = ""; 
        }

        public void newRegex(int id, string start, int topLayerId)
        {
            Regex regex = new Regex(id, start, topLayerId);
            regexList.Add(regex); 
        }

        public void addLetter(int id, string letter)
        {
            regexList[id].addLetter(letter); 
        }

        public int getTopLayer(int id)
        {
            return regexList[id].topLayerId; 
        }

        public void addToAlphabet(char c)
        {
            this.Alphabet += c; 
        }

       
        public void startThompson()
        {
            this.Thompson = new Thompson(this.Alphabet.ToCharArray());
            ThompsonRead(0, 1, 0, 1);
            Thompson.finishThompson(); 
            Thompson.drawThompson();
        }
        public bool captureClose = false; 
        public int ThompsonRead(int id, int begin, int end, int list)
        {
            
            regexList[id].checkOr();
            if (regexList[id].Orlist.Count > 1)
            {
                int list2 = list;
                list = Thompson.or(begin, end, list);
                end = list2 + 2 ;
                begin = list2 + 1; 
            }
            foreach (string or in regexList[id].Orlist)
            {
                int begin2 = begin;
                int end2 = 0; 
                string before = "";
                for (int i =0; i < or.Length; i++)
                {
                    if(i >= (or.Length- 1))
                    {
                        end2 = end;
                        
                    }
                    switch (or[i])
                    {
                       
                        case '*':
                            int list2 = list; 
                            list = printLetters(before, begin2, end2, list,true);
                            if (list != list2)
                            {

                                begin2 = list;
                            }           
                            list = Thompson.star(begin2, end2, list);
                            int newEnd = list; 
                            
                            if (end2 > 0)
                            {
                                list = checkCap(before, list - 2, list-1, list);
                            }
                            else
                            {
                                list = checkCap(before, list - 3, list - 2, list);
                            }
                            begin2 = newEnd;                          
                            before = "";
                            break;
                        case '+':
                            int list3 = list;
                            list = printLetters(before, begin2, end2, list, true);
                            if (list != list3)
                            {

                                begin2 = list;
                            }
                            list = Thompson.plus(begin2, end2, list);
                            int newEnd2 = list;
                           
                            if (end2 > 0)
                            {
                                list = checkCap(before, list - 1, list, list);
                            }
                            else
                            {
                                list = checkCap(before, list - 2, list -1, list);
                            }
                            begin2 = newEnd2;
                            before = "";
                            break;
                        case '.':
                            break;
                        default:
                            before += or[i];
                            break;
                    }
                   
                }
                if (before.Length > 0)
                {
                    list = printLetters(before, begin2, end2 -1, list, false);
                    if (before.EndsWith('('))
                    {
                        int capId = int.Parse(before[before.Length - 2].ToString());
                        list = ThompsonRead(capId, list  , end2 , list);
                        captureClose = true;
                    }

                    before = ""; 
                }
            }
            return list; 
        }

        public int printLetters(string before, int begin, int end, int list, bool affector)
        {
 
            if (before.EndsWith('(') && before.Length > 2)
            {
                if (before.Length > 2)
                {
                    
                    list = Thompson.terminaal(begin, list, before[0]);
                    for (int i = 1; i < before.Length - 2; i++)
                    {
                        list = Thompson.terminaal(list, list, before[i]);
                    }
                   
                }
                

            }
            else if(before.Length > 1 && !before.EndsWith('('))
            {
             
                
                if (affector)
                {
                    if (before.Length <= 2)
                    {
                        
                       
                        list = Thompson.terminaal(begin, list, before[0]);
                    }
                    else
                    {
                        list = Thompson.terminaal(begin, list, before[0]);
                        for (int i = 1; i < before.Length - 2; i++)
                        {
                            list = Thompson.terminaal(list, list, before[i]);
                        }
                        if (end > 0 && before.Length > 1)
                        {
                            Thompson.terminaal(list, end, before[before.Length - 2]);
                        }
                        else if (before.Length > 1)
                        {
                            list = Thompson.terminaal(list, list, before[before.Length - 2]);
                        }
                    }
                }
                else
                {
                    list = Thompson.terminaal(begin, list, before[0]);
                    for (int i = 1; i < before.Length - 1; i++)
                    {
                        list = Thompson.terminaal(list, list, before[i]);
                    }
                    if (end > 0 && before.Length > 1)
                    {
                        Thompson.terminaal(list, end, before[before.Length - 1]);
                    }
                    else if (before.Length > 1)
                    {
                        list = Thompson.terminaal(list, list, before[before.Length - 1]);
                    }
                }

               

            }
            else if (!affector)
            {

                Thompson.terminaal(begin, end, before[0]);
               
                
            }

            return list; 
           
        }

        public int checkCap(string before, int begin, int end, int list)
        {
            if (before.EndsWith('('))
            {
                int capId = int.Parse(before[before.Length - 2].ToString());
                list = ThompsonRead(capId, begin, end, list);
                captureClose = true; 
            }
            else
            {
                Thompson.terminaal(begin, end-1, before[before.Length - 1]);
            }
            return list;
        }

        public Automata<string> GetAutomata()
        {
            return Thompson.GetAutomata(); 
        }








    }
}
