using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    enum Affector
    {
        nul,
        or,
        plus,
        star,
        dot
    }
    class RegexData
    {
       
        public List <Regex> regexList;
        public List<string> captureList; 
        public RegexData()
        {
            this.regexList = new List<Regex>();
            this.captureList = new List<string>(); 
        }


        
        
        public void newRegex(int group, int id)
        {
            Regex regex = new Regex(id);
            regex.groupOfLetters = this.captureList[group - 1]; //1 = 0 
            if(this.captureList.Count > group)// 1 
            {
                regex.remainder = this.captureList[group]; //plaats 1
            }
            regexList.Add(regex); 
        }

        public void newAffecRegex(int id)
        {
            Regex regex = new Regex(id);
            regexList.Add(regex);
        }

        public void fillAffector(Affector affector)
        {
            this.regexList[regexList.Count-1].affector = affector; 
        }

        public void fillRemainder()
        {
            regexList[regexList.Count - 1].remainder = captureList[captureList.Count - 1]; 
        }

        public void addLettertoRegex(char letter)
        {
            this.regexList[regexList.Count - 1].groupOfLetters += letter; 
        }

        public void newCapture()
        {
            captureList.Add(""); 
        }

        public void addLettertoCapture(char letter, int group)
        {
            captureList[group - 1] += letter; 
        }

        public void CaptureClear()
        {
            captureList.Clear(); 
        }

        public void startThompson()
        {
            Thompson thompson = new Thompson();
            int list = 0;
            int stateRem = 0;
            bool OrDot = false;

            List<int> beginCap = new List<int>();
            List<int> endCap = new List<int>();
            int beginCapCount = 0;
            int endCapCount = 0;
            beginCap.Add(2);
            endCap.Add(0);

            int remember = 0;
            int beginStart = 0;
            int endStart = 0; 
            //regexList.Reverse(); 
            //regexList.Sort();
            for (int i = 0; i < regexList.Count; i++)
            {
                
                switch (regexList[i].affector)
                {
                    case Affector.nul:
                        foreach(char c in regexList[i].groupOfLetters)
                        {
                            remember = list; 
                            if (endCap[endCapCount] - 1 > 0)
                            {
                                endStart = endCap[1];
                                beginStart = endCap[endCapCount] - 1;
                            }
                            else
                            {
                                endStart = list;
                            }

                            

                            if (regexList[i].id < 99999)
                            {
                                list = thompson.epsilon(list, list + 1);
                                list = thompson.terminaal(beginStart+ 1,list, c);
                                list = thompson.epsilon(list, list + 1);
                                beginCapCount++;
                                endCapCount++;
                                beginCap.Add(remember);
                                endCap.Add(list);
                            }
                            else
                            {
                                list = thompson.terminaal(endStart,list, c);
                                beginCapCount = 0;
                                endCapCount = 0;
                                beginCap.Clear();
                                endCap.Clear();
                                beginCap.Add(0);
                                endCap.Add(0);
                            }
                        }
                        break;
                    case Affector.or:
                        remember = list;
                        list =thompson.or(beginCap[beginCapCount] +1, endCap[endCapCount] -1, list, regexList[i].groupOfLetters);
                       
                        break; 
                    case Affector.plus:
                        remember = list;
                        if (endCap[endCapCount] - 1 > 0)
                        {
                            beginStart = endCap[endCapCount] - 1;
                            endStart = endCap[1];
                        }
                        else
                        {
                            endStart = list;
                        }

                       
                        if (regexList[i].id < 99999)
                        {
                            list = thompson.plus(beginStart, endCap[endCapCount], list, regexList[i].groupOfLetters);
                            beginCapCount++;
                            endCapCount++;
                            beginCap.Add(remember);
                            endCap.Add(list);
                        }
                        else
                        {
                            list = thompson.plus(endStart, 0, list, regexList[i].groupOfLetters);
                            beginCapCount = 0;
                            endCapCount = 0;
                            beginCap.Clear();
                            endCap.Clear();
                            beginCap.Add(0);
                            endCap.Add(0);
                        }
                        break;
                    case Affector.star:
                        remember = list;
                        if (endCap[endCapCount] - 1 > 0)
                        {
                            beginStart = endCap[endCapCount] - 1;
                            endStart = endCap[1];
                        }
                        else
                        {
                            endStart = list; 
                        }
                        
                        if (regexList[i].id < 99999)
                        {
                            list = thompson.star(beginStart, endCap[endCapCount], list, regexList[i].groupOfLetters);
                            beginCapCount++;
                            endCapCount++;
                            beginCap.Add(remember);
                            endCap.Add(list);
                        }
                        else
                        {
                            list = thompson.star(endStart, 0, list, regexList[i].groupOfLetters);
                            beginCapCount = 0;
                            endCapCount = 0;
                            beginCap.Clear();
                            endCap.Clear(); 
                            beginCap.Add(0);
                            endCap.Add(0);
                        }
                        break;
                    case Affector.dot:
                        list = thompson.dot(beginCap[beginCapCount]+1, endCap[endCapCount] - 1, list -1, regexList[i].groupOfLetters);

                        break; 



                }
            }

            thompson.drawThompson(); 
        }

    

        
        

    }
}
