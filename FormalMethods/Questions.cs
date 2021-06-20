using System;
using System.Collections.Generic;
using System.Text;

namespace FormalMethods
{
    public class Questions
    {
        public string Question { get; set; }
        public string answer { get; set; }
        public string ans1 { get; set; }
        public string ans2 { get; set; }
        public string ans3 { get; set; }
        public string ans4 { get; set; }
        public string type { get; set; }

        public Questions(string Question, string answer, string ans1, string ans2, string ans3, string ans4, string type)
        {
            this.Question = Question;
            this.answer = answer;
            this.ans1 = ans1;
            this.ans2 = ans2;
            this.ans3 = ans3;
            this.ans4 = ans4;
            this.type = type;
        }
    }
}
