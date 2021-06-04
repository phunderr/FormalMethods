using System;
using System.Text;

namespace FormalMethods
{
    public class State : IComparable
    {
        public string Name { get; set; }
        public bool StartState { get; set; }
        public bool EndState { get; set; }

        public State(string name, bool start, bool end)
        {
            Name = name;
            StartState = start;
            EndState = end;
        }

        public State(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (obj is State)
            {
                State other = (State)obj;
                return Name.CompareTo(other.Name) == 0 &&
                StartState == other.StartState &&
                EndState == other.EndState;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return $"(name = {Name},Startstate: {StartState}, endstate: {EndState}";
        }


        public int CompareTo(object obj)
        {

            byte[] selfarr = Encoding.ASCII.GetBytes(Name);
            int selfval = (int)selfarr[0];

            State other = (State)obj;
            byte[] otherarr = Encoding.ASCII.GetBytes(other.Name);
            int otherval = (int)otherarr[0];

            return selfval - otherval;
        }
    }
}
