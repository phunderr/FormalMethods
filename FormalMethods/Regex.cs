using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Regex : IComparable<Regex>
    {
        public string groupOfLetters { get; set; }
        public string remainder { get; set; }
        public Affector affector { get; set; }

        public int id { get; set; }

        public int count { get; set; }

        public Regex(int id)
        {
            this.id = id; 
            this.affector = new Affector();

            count = 0; 
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

        public int CompareTo(Regex obj)
        {
            if (this.id > obj.id) return 1;
            if (this.id < obj.id) return -1;
            if (this.id == obj.id)
            {
                if (this.affector == Affector.nul) return -1;
                if (this.affector == Affector.star && obj.affector != Affector.nul ) return -1;
                if (this.affector == obj.affector) return 0;
                if (this.affector == Affector.plus && obj.affector != Affector.nul) return -1;
                if (this.affector == Affector.or && obj.affector == Affector.dot) return 0;
                





                return 1;
            }
            return 0;
        }
    }
}
