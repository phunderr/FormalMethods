using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;


namespace FormalMethods
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();



            List<string> lijst = GenerateStrings.GenerateString(8, "ab");

            foreach (string element in lijst)
            {
                Debug.WriteLine(element.ToString());
            }

            char[] alphabet = { 'a', 'b' };
            Automata<string> automata = new Automata<string>(alphabet);

            automata.AddTransition(new Transition<string>("0", 'a', "1"));
            automata.AddTransition(new Transition<string>("0", 'b', "4"));

            automata.AddTransition(new Transition<string>("1", 'a', "4"));
            automata.AddTransition(new Transition<string>("1", 'b', "2"));

            automata.AddTransition(new Transition<string>("2", 'a', "4"));
            automata.AddTransition(new Transition<string>("2", 'b', "3"));

            automata.AddTransition(new Transition<string>("3", 'a', "3"));
            automata.AddTransition(new Transition<string>("3", 'b', "3"));

            automata.AddTransition(new Transition<string>("4", 'a', "4"));
            automata.AddTransition(new Transition<string>("4", 'b', "5"));

            automata.AddTransition(new Transition<string>("5", 'a', "6"));
            automata.AddTransition(new Transition<string>("5", 'b', "5"));

            automata.AddTransition(new Transition<string>("6", 'a', "7"));
            automata.AddTransition(new Transition<string>("6", 'b', "5"));

            automata.AddTransition(new Transition<string>("7", 'a', "4"));
            automata.AddTransition(new Transition<string>("7", 'b', "8"));

            automata.AddTransition(new Transition<string>("8", 'a', "4"));
            automata.AddTransition(new Transition<string>("8", 'b', "5"));

            automata.DefineAsStartState("0");

            automata.DefineAsFinalState("3");
            automata.DefineAsFinalState("8");


            List<string> valid = new List<string>();
            List<string> invalid = new List<string>();

            foreach (string test in lijst) 
            {
                if (automata.Accept(test)) 
                {
                    valid.Add(test);
                }
                else 
                {
                    invalid.Add(test);
                }
            }

            



            Grapher grapher = new Grapher();
            grapher.CreateGraph(automata, "test");




        }
    }
}
