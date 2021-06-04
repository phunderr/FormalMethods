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
