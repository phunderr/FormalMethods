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

        
        public void newRegex(int group)
        {
            Regex regex = new Regex();
            regex.groupOfLetters = this.captureList[group - 1]; //1 = 0 
            if(this.captureList.Count > group)// 1 
            {
                regex.remainder = this.captureList[group]; //plaats 1
            }
            regexList.Add(regex); 
        }

        public void fillAffector(Affector affector)
        {
            this.regexList[regexList.Count-1].affector = affector; 
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

        
        public void NormalGroup()
        {

        }
    }
}
