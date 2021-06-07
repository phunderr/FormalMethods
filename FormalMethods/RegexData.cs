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

            regexList.Reverse(); 
            for(int i = 0; i < regexList.Count; i++)
            {
                
                switch (regexList[i].affector)
                {
                    case Affector.nul:
                        foreach(char c in regexList[i].groupOfLetters)
                        {
                            list = thompson.terminaal(list, c); 
                        }
                        break;
                    case Affector.or:

                        list =thompson.or(list, regexList[i].groupOfLetters, regexList[i].remainder);
                        break; 
                    case Affector.plus:

                        list = thompson.plus(list, regexList[i].groupOfLetters);
                        regexList[i].count = list;
                        break;
                    case Affector.star:
                        list = thompson.star(list, regexList[i].groupOfLetters); 
                        break;
                    case Affector.dot:
                        list = thompson.dot(list, regexList[i].groupOfLetters); 
                        break; 



                }
            }
        }

        
        

    }
}
