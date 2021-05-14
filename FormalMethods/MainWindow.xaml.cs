using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;


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



            List<string> lijst = GenerateStrings.GenerateString(2, "ab");

            foreach (string element in lijst)
            {
                Debug.WriteLine(element.ToString());
            }

            char[] alphabet = { 'a', 'b' };
            Automata<string> automata = new Automata<string>(alphabet);

            automata.AddTransition(new Transition<string>("0", 'a',"1"));
            automata.AddTransition(new Transition<string>("0", 'b',"4"));

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

            Grapher grapher = new Grapher();
            grapher.CreateGraph(automata, "test");
            grapher.SaveToPDF();



        }
    }
}
