using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    class Regex
    {
        public string letters;
        public string expression; 

        public void FillLetters(string letters)
        {
            this.letters = letters; 
        }

        public void addLetter(string letter)
        {
            this.letters += letter; 
        }

        public void SetExpression(string expression)
        {
            this.expression = expression; 
        }



     }
}
