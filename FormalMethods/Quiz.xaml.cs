using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Text.RegularExpressions;

namespace FormalMethods
{
    /// <summary>
    /// Interaction logic for Quiz.xaml
    /// </summary>
    public partial class Quiz : Window
    {

        private List<Questions> Questionlist;
        private int count = 0;
        System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("m2|m3|m4");

        private int ScoreCount = 0;

        public Quiz()
        {
            Questionlist = new List<Questions>();
            InitializeComponent();
            LoadQuestions();
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex("m2|m3|m4");
            if (reg.IsMatch(Questionlist[0].type))
            {
                InputView.Children.Add(new QuizMultipleChoiceView(Questionlist[0]));
                txtQuestion.Text = Questionlist[0].Question;
            }
            //setup dispatch timer
            DispatcherTimer time = new DispatcherTimer();
            time.Tick += new EventHandler(Timer_tick);
            time.Interval = new TimeSpan(0, 0, 0, 0, 100);
            time.Start();

        }

        public void Timer_tick(object sender, EventArgs e)
        {
            if (((QuizMultipleChoiceView)InputView.Children[0]).Answered)
            {
                if (count > 10)
                {
                    this.Close();
                }

                if (((QuizMultipleChoiceView)InputView.Children[0]).Correct)
                {
                    ScoreCount++;
                    scoreText.Content = "Score: " + ScoreCount + " / 10";
                }

                count++;
                //count %= Questionlist.Count;
                InputView.Children.Clear();
                if (reg.IsMatch(Questionlist[count % Questionlist.Count].type))
                    InputView.Children.Add(new QuizMultipleChoiceView(Questionlist[count % Questionlist.Count]));
                else
                    InputView.Children.Add(new QuizOpenAnswerView(Questionlist[count % Questionlist.Count]));

                txtQuestion.Text = Questionlist[count % Questionlist.Count].Question;

            }



        }

        public void LoadQuestions()
        {
            JArray  JsonString = JArray.Parse(File.ReadAllText("../../../res/questions.json"));


            foreach(JToken token in JsonString)
            {
                Questionlist.Add(token.ToObject<Questions>());

            }
        }
    }
}
