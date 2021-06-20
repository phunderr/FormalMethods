using System;



namespace FormalMethods
{
    public class Transition<T> : IComparable<Transition<T>> where T : IComparable
    {
        public readonly static char EPSILON = '$';
        public State from { get; set; }
        public char symbol { get; set; }
        public State to { get; set; }



        //public T FromState { get; private set; }
        //public T ToState { get; private set; }
        //public char Symbol { get; private set; }



        //public Transition(T fromOrTo, char s) : this(from, s, to)
        //{ }



        //public Transition(T from, T to) : this(from, EPSILON, to)
        //{ }



        //public Transition(T from, char s, T to)
        //{
        //    FromState = from;
        //    Symbol = s;
        //    ToState = to;
        //}



        public Transition(State from, char s, State to)
        {
            this.from = from;
            this.symbol = s;
            this.to = to;
        }



        public override int GetHashCode()
        {
            return base.GetHashCode();
        }



        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            else if (obj is Transition<T>)
            {
                Transition<string> transition = (Transition<string>)obj;
                return from.CompareTo(transition.from) == 0 &&
                to.CompareTo(transition.to) == 0 &&
                symbol == transition.symbol;
            }



            return false;
        }



        public int CompareTo(Transition<T> other)
        {
            int fromCompare = from.Equals(other.from) ? 1 : 0;
            int symbolCompare = symbol.CompareTo(other.symbol);
            int toCompare = to.Equals(other.to) ? 1 : 0;



            return fromCompare != 0 ? fromCompare : (symbolCompare != 0 ? symbolCompare : toCompare);
        }



        public void reverse()
        {
            State tempfrom = from;
            from = to;
            to = tempfrom;
        }



        public string toString()
        {
            return $"({from}, {symbol}) --> {to}";
        }
    }
}