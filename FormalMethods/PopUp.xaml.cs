using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FormalMethods
{
    /// <summary>
    /// Interaction logic for PopUp.xaml
    /// </summary>
    public partial class PopUp : Window
    {

        public event EventHandler ActionEvent;

        public PopUp()
        {
            InitializeComponent();
        }

        private void DFA_Click(object sender, RoutedEventArgs e)
        {
            ActionEvent?.Invoke(sender, e);
        }
    }
}
