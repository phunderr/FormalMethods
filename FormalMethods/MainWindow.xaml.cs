using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace FormalMethods
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<UserControl> Input { get; set; }

        public MainWindow()
        {
            Input = new List<UserControl>();
            InitializeComponent();
            DispatcherTimer time = new DispatcherTimer();
            time.Tick += new EventHandler(Timer_tick);
            time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            time.Start();

            RegexParser regexParser = new RegexParser();
            if (!regexParser.ParseRegex("a+a|a%(a|bc(ab*))*ab"))
            {
                this.Close();
            }



            //regexParser.ParseRegex("(abbaa)*");
            //regexParser.ParseRegex("aa|bb|ccd*c");
            //a
            //regexParser.ParseRegex("abbb");
            //regexParser.ParseRegex("(ab)*|(bc)+");



            /*List<string> lijst = GenerateStrings.GenerateString(8, "ab");

            foreach (string element in lijst)
            {
                Debug.WriteLine(element.ToString());
            }

            char[] alphabet = { 'a', 'b' };
            Automata<string> automata = new Automata<string>(alphabet);




            automata.AddTransition(new Transition<string>(new State("0"), 'a', new State("1")));
            automata.AddTransition(new Transition<string>(new State("0"), 'b', new State("4")));

            automata.AddTransition(new Transition<string>(new State("1"), 'a', new State("4")));
            automata.AddTransition(new Transition<string>(new State("1"), 'b', new State("2")));
            
            automata.AddTransition(new Transition<string>( new State("2"), 'a',new State("4")));
            automata.AddTransition(new Transition<string>(new State("2"), 'b', new State("3")));

            automata.AddTransition(new Transition<string>(new State("3"), 'a', new State("3")));
            automata.AddTransition(new Transition<string>(new State("3"), 'b', new State("3")));

            automata.AddTransition(new Transition<string>(new State("4"), 'a', new State("4")));
            automata.AddTransition(new Transition<string>(new State("4"), 'b', new State("5")));

            automata.AddTransition(new Transition<string>(new State("5"), 'a', new State("6")));
            automata.AddTransition(new Transition<string>(new State("5"), 'b', new State("5")));

            automata.AddTransition(new Transition<string>(new State("6"), 'a', new State("7")));
            automata.AddTransition(new Transition<string>(new State("6"), 'b', new State("5")));
            

            automata.AddTransition(new Transition<string>(new State("7"), 'a', new State("4")));
            automata.AddTransition(new Transition<string>(new State("7"), 'b', new State("8")));

            automata.AddTransition(new Transition<string>(new State("8"), 'a', new State("4")));
            automata.AddTransition(new Transition<string>(new State("8"), 'b', new State("5")));

            automata.DefineAsStartState(new State("0"));

            automata.DefineAsFinalState(new State("3"));
            automata.DefineAsFinalState(new State("8"));



            



            Automata<string> a2 = new Automata<string>(alphabet);

            a2.AddTransition(new Transition<string>(new State("0"), 'a', new State("1")));
            a2.AddTransition(new Transition<string>(new State("0"), 'b', new State("a")));

            a2.AddTransition(new Transition<string>(new State("1"), 'a', new State("1")));
            a2.AddTransition(new Transition<string>(new State("1"), 'b', new State("0")));

            
            a2.DefineAsStartState(new State("0"));

            a2.DefineAsFinalState(new State("0"));

            





            Automata<string> a3 = new Automata<string>(alphabet);

            a3.AddTransition(new Transition<string>(new State("S"), 'a', new State("S")));
            a3.AddTransition(new Transition<string>(new State("S"), 'a', new State("A")));

            a3.AddTransition(new Transition<string>(new State("A"), 'b', new State("F")));
            a3.DefineAsStartState(new State("S"));

            a3.DefineAsFinalState(new State("F"));

            a3.toDFA();

            //List<string> valid = new List<string>();
            //List<string> invalid = new List<string>();

            //foreach (string test in lijst) 
            //{
            //    if (automata.AcceptDFA(test)) 
            //    {
            //        valid.Add(test);
            //    }
            //    else 
            //    {
            //        invalid.Add(test);
            //    }
            //}




            Debug.WriteLine("Valid 1 CHECK AND 2");
            List<string> validAND = new List<string>();
            List<string> invalidAND = new List<string>();

            foreach (string test in lijst)
            {
                if (Checker.AND(automata, a2,test))
                {
                    validAND.Add(test);
                }
                else
                {
                    invalidAND.Add(test);
                }
            }

            foreach (string s in validAND) 
            {
                Debug.WriteLine("Valid: "+ s);
            }

            foreach (string s in invalidAND)
            {
                Debug.WriteLine("inValid: " + s);
            }


            Debug.WriteLine("Valid 1 CHECK OR 2");
            List<string> validOR = new List<string>();
            List<string> invalidOR = new List<string>();

            foreach (string test in lijst)
            {
                if (Checker.OR(automata, a2, test))
                {
                    validOR.Add(test);
                }
                else
                {
                    invalidOR.Add(test);
                }
            }


            foreach (string s in validOR)
            {
                Debug.WriteLine("Valid: " + s);
            }

            foreach (string s in invalidOR)
            {
                Debug.WriteLine("inValid: " + s);
            }

            Debug.WriteLine("is DFA?" + automata.isDFA());

            automata.reverse();



            Grapher grapher = new Grapher();
            grapher.CreateGraph(automata, "test");


*/

        }

        private void Timer_tick(object sender, EventArgs e)
        {
            if (GrammarBox.IsChecked.Value)
            {
                GrammarButton.Visibility = Visibility.Visible;
                RegexButton.Visibility = Visibility.Hidden;
                AutomataButton.Visibility = Visibility.Hidden;
            }

            else if (RegexBox.IsChecked.Value)
            {
                GrammarButton.Visibility = Visibility.Hidden;
                RegexButton.Visibility = Visibility.Visible;
                AutomataButton.Visibility = Visibility.Hidden;


            }

            else if (AutomataBox.IsChecked.Value)
            {
                GrammarButton.Visibility = Visibility.Hidden;
                RegexButton.Visibility = Visibility.Hidden;
                AutomataButton.Visibility = Visibility.Visible;


            }



        }

        private void RegexButton_Click(object sender, RoutedEventArgs e)
        {
            if (Input.Count != 0)
            {
                if (!(Input.First() is RegexView))
                {
                    Input.Clear();
                    InputPanel.Children.Clear();
                }
            }

            UserControl control = new RegexView();
            Input.Add(control);
            InputPanel.Children.Add(control);
        }

        private void AutomataButton_Click(object sender, RoutedEventArgs e)
        {
            // not sure if this needs to be a function yet
        }

        private void GrammarButton_Click(object sender, RoutedEventArgs e)
        {
            if (Input.Count != 0)
            {
                    if (!(Input.First() is TransitionView))
                {
                    Input.Clear();
                    InputPanel.Children.Clear();
                }
            }
            UserControl control = new TransitionView();
            Input.Add(control);
            InputPanel.Children.Add(control);
        }
    }
}
