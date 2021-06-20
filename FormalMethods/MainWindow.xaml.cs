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
        private PopUp popup = new PopUp();
        RegexParser regexParser = new RegexParser();
        private bool IsRegex = false;
        Automata<string> automata = null;

        public MainWindow()
        {
            Input = new List<UserControl>();
            InitializeComponent();
            DispatcherTimer time = new DispatcherTimer();
            time.Tick += new EventHandler(Timer_tick);
            time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            time.Start();
            
            

            popup.ActionEvent += DFAOrMin;

            List<string> lijst = GenerateStrings.GenerateString(8, "ab");

            foreach (string element in lijst)
            {
                Debug.WriteLine(element.ToString());
            }

            char[] alphabet = { 'a', 'b' };
            //Automata<string> automata = new Automata<string>(alphabet);


            //List<string> valid = new List<string>();
            //List<string> invalid = new List<string>();


            //automata.AddTransition(new Transition<string>(new State("S"), 'a', new State("A")));
            //automata.AddTransition(new Transition<string>(new State("S"), 'a', new State("S")));

            //automata.AddTransition(new Transition<string>(new State("A"), 'b', new State("F")));

            //automata.DefineAsStartState(new State("S"));
            //automata.DefineAsFinalState(new State("F"));

            //Automata<string> am2 = automata.toDFA();

            //am2.minimaliseren();

            Automata<string> automata = new Automata<string>(alphabet);


            List<string> valid = new List<string>();
            List<string> invalid = new List<string>();


            automata.AddTransition(new Transition<string>(new State("1"), 'a', new State("2")));
            automata.AddTransition(new Transition<string>(new State("1"), 'b', new State("2")));

            automata.AddTransition(new Transition<string>(new State("1"), 'b', new State("3")));

            automata.AddTransition(new Transition<string>(new State("2"), 'a', new State("3")));
            automata.AddTransition(new Transition<string>(new State("2"), 'b', new State("4")));
            automata.AddTransition(new Transition<string>(new State("2"), 'ε', new State("4")));

            automata.AddTransition(new Transition<string>(new State("3"), 'a', new State("2")));
            

            automata.AddTransition(new Transition<string>(new State("4"), 'a', new State("2")));
            automata.AddTransition(new Transition<string>(new State("4"), 'a', new State("5")));

            automata.AddTransition(new Transition<string>(new State("5"), 'b', new State("5")));
            automata.AddTransition(new Transition<string>(new State("5"), 'ε', new State("3")));

            automata.DefineAsStartState(new State("1"));
            automata.DefineAsFinalState(new State("2"));
            automata.DefineAsFinalState(new State("3"));

            automata.AcceptNDFA("aaaaa");
            Automata<string> am2 = automata.toDFA();

           


            Grapher grapher = new Grapher();
            grapher.CreateGraph(am2, "test");




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
            
            Input.Clear();
            InputPanel.Children.Clear();
            

            RegexView control = new RegexView();
            Input.Add(control);
            InputPanel.Children.Add(control);
            IsRegex = true;
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
            IsRegex = false;
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            Window w = popup;
            w.Show();

             automata = new Automata<string>(Alphabet.Text.ToCharArray());
            string rege = string.Empty;
            //Grammar to automata
            foreach (Object control in InputPanel.Children)
            {
                //Grammar
                if (control.GetType().Equals(typeof(TransitionView)))
                {
                    char[] c = (control as TransitionView).Character.Text.ToCharArray();
                    automata.AddTransition(new Transition<string>(new State((control as TransitionView).FromState.Text), c[0], new State((control as TransitionView).ToState.Text)));

                }
                if (control.GetType().Equals(typeof(RegexView)))
                {
                    rege = (control as RegexView).RegexIn.Text;
                }

            }

            if (!IsRegex)
            {
                if (InputPanel.Children[0].GetType().Equals(typeof(TransitionView)))
                {
                    char[] start = StartStateInput.Text.ToCharArray();
                    foreach (char c in start)
                        automata.DefineAsStartState(new State(c.ToString()));

                    char[] end = EndStatesInput.Text.ToCharArray();
                    foreach (char c in end)
                        automata.DefineAsFinalState(new State(c.ToString()));
                }


                List<string> lijst = GenerateStrings.GenerateString(8, Alphabet.Text);
                List<string> accept = new List<string>();
                List<string> notaccept = new List<string>();
                foreach (string s in lijst)
                {
                    if (automata.AcceptNDFA(s))
                    {
                        accept.Add(s);
                    }
                    else
                    {
                        notaccept.Add(s);
                    }
                }

                foreach (string acc in accept)
                    Accept.Text += (acc + "\n");

                foreach (string nacc in notaccept)
                    Notaccept.Text += (nacc + "\n");

            }

            //Regex
            if(IsRegex)
                regexParser.ParseRegex(rege);

            //TODO show Graphiz file
            
        }

        private void Quiz_btn_Click(object sender, RoutedEventArgs e)
        {
            Window quiz = new Quiz();
            quiz.Show();
        }


        private void DFAOrMin(object Sender, EventArgs e)
        {
            if((Sender as Button).Name.Equals("Minimize"))
            {
                if(IsRegex)
                {
                    automata = regexParser.GetAutomata();
                }
                automata = automata.minimaliseren();
                Grapher grapher = new Grapher();
                grapher.CreateGraph(automata, "test");
            }
            else if ((Sender as Button).Name.Equals("DFA"))
            {
                if (IsRegex)
                {
                    automata = regexParser.GetAutomata();
                }
                automata = automata.toDFA();
                Grapher grapher = new Grapher();
                grapher.CreateGraph(automata, "test");

            }
        }
    }
}
