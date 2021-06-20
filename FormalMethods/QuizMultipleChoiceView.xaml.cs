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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FormalMethods
{
    /// <summary>
    /// Interaction logic for QuizMultipleChoiceView.xaml
    /// </summary>
    public partial class QuizMultipleChoiceView : UserControl
    {

        public bool Answered = false;
        public bool Correct = false;
        public Questions question;

        public QuizMultipleChoiceView(Questions questions)
        {
            this.question = questions;
            InitializeComponent();

            if (questions.type.Equals("m2") )
            {
                ans3.Visibility = Visibility.Hidden;
                ans4.Visibility = Visibility.Hidden;
            }
            else if (questions.Equals("m3"))
            {
                ans3.Visibility = Visibility.Visible;
                ans4.Visibility = Visibility.Hidden;

            }
            else 
            {
                ans3.Visibility = Visibility.Visible;
                ans4.Visibility = Visibility.Visible;
            }

            ans1.Content = questions.ans1;
            ans2.Content = questions.ans2;

            ans3.Content = questions.ans3;

            ans4.Content = questions.ans4;


        }

        private void CheckAnswer(object sender, EventArgs e)
        {
            if(((Button)sender).Name.Equals(question.answer))
            {
                this.Answered = true;
                Correct = true;
            }
            else
            {
                Correct = false;
                Answered = true;
            }
        }
    }
}
