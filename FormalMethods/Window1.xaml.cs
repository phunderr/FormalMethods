using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            Height = image.Height;
            Width = image.Width;
            image.Source = null;
            Uri resourceUri = new Uri("bin/Debug/netcoreapp3.1/test.dot.png", UriKind.Relative);

            image.Source = new BitmapImage(resourceUri);
                


            //image.Source = "test.dot.svg";
        }
    }
}
