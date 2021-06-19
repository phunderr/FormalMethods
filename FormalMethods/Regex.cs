using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Regex
    {
        public string groupOfLetters { get; set; }
        public int layerId { get; set; }

        public int topLayerId { get; set; }

        public List<string> Orlist { get; set; }


        public Regex(int id, string start, int topLayerId)
        {
            this.layerId = id;
            this.groupOfLetters = start;
            this.topLayerId = topLayerId; 
        }
        public Regex(string groupOfLetters)
        {
            this.groupOfLetters = groupOfLetters; 
        }
      
        public void addLetter(string letter)
        {
            this.groupOfLetters += letter; 
        }

        public void checkOr()
        {
            Orlist = new List<string>();
            Orlist.Add("");
            int count = 0;
            foreach (char c in this.groupOfLetters)
            {
                switch (c)
                {
                    case '|':
                        Orlist.Add("");
                        count++;
                        break;

                    default: //no special character detected
                        Orlist[count] += c;
                        break;


                }
            }

        }


    }
}
