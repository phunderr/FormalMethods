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
                return Name.CompareTo(other.Name) == 0;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public string ToString(bool full)
        {
            return $"(name = {Name},Startstate: {StartState}, endstate: {EndState}";
        }

        public override string ToString()
        {
            return Name;
        }

        public int CompareTo(object obj)
        {
            State other = (State)obj;

           return Name.CompareTo(other.Name);
        }

       
    }
}
