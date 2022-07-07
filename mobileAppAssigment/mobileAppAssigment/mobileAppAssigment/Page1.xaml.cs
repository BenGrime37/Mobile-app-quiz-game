using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileAppAssigment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public static class MyGlobals
        {
            public static int level;
            public static int timerTime = 0;
            public static int num1;
            public static int num2;
            public static int answer;
            public static string symbol;
            public static int score;
            public static int questionsAnswered = 0;
        }
        private void spawnQuestions()
        {
            Random rnd = new Random();
            MyGlobals.num1 = rnd.Next(1, 12);
            MyGlobals.num2 = rnd.Next(1, 12);
            int symbolNumber = rnd.Next(1, 4);
            if (symbolNumber == 1)
            {
                MyGlobals.symbol = "+";
                MyGlobals.answer = MyGlobals.num1 + MyGlobals.num2;
            }
            else if (symbolNumber == 2)
            {
                MyGlobals.symbol = "-";
                MyGlobals.answer = MyGlobals.num1 - MyGlobals.num2;
            }
            else if (symbolNumber == 3)
            {
                MyGlobals.symbol = "x";
                MyGlobals.answer = MyGlobals.num1 * MyGlobals.num2;
            }
            else if (symbolNumber == 4)
            {
                MyGlobals.symbol = "÷";
                MyGlobals.answer = MyGlobals.num1 / MyGlobals.num2;
            }

            questionTxt.Text = MyGlobals.num1 + MyGlobals.symbol + MyGlobals.num2;

            if (MyGlobals.answer < 0 || MyGlobals.answer % 1 != 0 || MyGlobals.answer != 0.5)//if answer is less than 0 OR answer not a whole number And answer is not a half
            {
                spawnQuestions();
            }
            else
            {
                //nothing, answer is valid
            }

        }
        public Page1(int level)
        {
            InitializeComponent();
            MyGlobals.level = level;

            if (MyGlobals.level == 1)
            {
                MyGlobals.timerTime = 10;
            }
            else if (MyGlobals.level == 2)
            {
                MyGlobals.timerTime = 20;
            }

            spawnQuestions();
        }

        private void submitBtn_Clicked(object sender, EventArgs e)
        {
            if (MyGlobals.questionsAnswered == 10)
            {
                if (MyGlobals.timerTime == 0)
                {
                    if (MyGlobals.answer == Int32.Parse(enterTxt.Text))
                    {
                        correctOrNot.Text = "correct";
                        MyGlobals.score += 1;
                        MyGlobals.questionsAnswered += 1;
                        spawnQuestions();
                    }
                    else
                    {
                        correctOrNot.Text = "incorrect";
                        MyGlobals.questionsAnswered += 1;
                        spawnQuestions();
                    }
                }
                else
                {
                    Task.Run(async () =>
                    {
                        while ((true))
                        {
                            await Device.InvokeOnMainThreadAsync(() =>
                            {
                                if (int.Parse(timerLbl.Text) < MyGlobals.timerTime)
                                {
                                    timerLbl.Text = "" + (int.Parse(timerLbl.Text) + 1);
                                }
                                else
                                {
                                    spawnQuestions();
                                }
                            });
                            await Task.Delay(1000);
                        }
                    });

                    if (MyGlobals.answer == Int32.Parse(enterTxt.Text))
                    {
                        correctOrNot.Text = "correct";
                        MyGlobals.score += 1;
                        MyGlobals.questionsAnswered += 1;
                        spawnQuestions();
                    }
                    else
                    {
                        correctOrNot.Text = "incorrect";
                        MyGlobals.questionsAnswered += 1;
                        spawnQuestions();
                    }
                }
            }
        }
        //save score to database and end game
    }
}
