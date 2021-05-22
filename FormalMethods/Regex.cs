using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Regex
    {
        public string groupOfLetters { get; set; }
        public string remainder { get; set; }
        public Affector affector { get; set; }

        public Regex()
        {
      
        }
        public Regex(string groupOfLetters)
        {
            this.groupOfLetters = groupOfLetters; 
        }
      
        public Regex(string groupOfLetters, Affector affector)
        {
            this.groupOfLetters = groupOfLetters;
            this.affector = affector; 
        }

        public Regex(string groupOfLetters, string remainder, Affector affector)
        {
            this.groupOfLetters = groupOfLetters;
            this.remainder = remainder;
            this.affector = affector; 
        }
    }
}
