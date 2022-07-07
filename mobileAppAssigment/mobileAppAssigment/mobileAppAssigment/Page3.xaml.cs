using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using System.IO;
using Plugin.SimpleAudioPlayer;
using Xamarin.Forms.Xaml;

namespace mobileAppAssigment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page3 : ContentPage
    {
        private ISimpleAudioPlayer _simpleAudioPlayer;
        public static class MyGlobals
        {
            public static int level;
            public static int timerTime = 0;
            public static float num1;
            public static float num2;
            public static float answer;
            public static string symbol;
            public static int score = 0;
            public static int questionsAnswered = 1;
            public static Random rnd;
            public static int symbolNumber;
            public static float[] answers;
            public static float[] answerCorrect;
            public static int i = 0;
            public static int j = 0;
        }
        
        public Page3(int level)
        {
            InitializeComponent();

            MyGlobals.answers = new float[20];
            MyGlobals.answerCorrect = new float[20];
            MyGlobals.level = level;

            if (MyGlobals.level == 1)
            {
                MyGlobals.timerTime = 20;
            }
            else if (MyGlobals.level == 2)
            {
                MyGlobals.timerTime = 10;
            }
            else
            {
                MyGlobals.timerTime = 0;
            }
            if (MyGlobals.timerTime != 0)
            {
                Task.Run(async () =>//this is the timer
                {
                    while ((true))
                    {
                        await Device.InvokeOnMainThreadAsync(() =>
                        {
                            if (int.Parse(timerLbl.Text) < MyGlobals.timerTime)
                            {
                                timerLbl.Text = "" + (int.Parse(timerLbl.Text) + 1);
                            }
                            else if (int.Parse(timerLbl.Text) == MyGlobals.timerTime)
                            {
                                timerLbl.Text = "0";
                                spawnQuestions();
                            }

                        });
                        if (int.Parse(timerLbl.Text) == MyGlobals.timerTime)
                        {


                        }
                        await Task.Delay(1000);



                    }
                });
            }
                

            spawnQuestions();
            
        }
        
        private void submitBtn_Clicked(object sender, EventArgs e)
        {

            if (MyGlobals.questionsAnswered < 10)//have all the questions bee answerd?
            {
                timerLbl.Text = "0";
                

                if (MyGlobals.timerTime == 0)//is there a timer depenidng on the level they want
                {

                    string StringAnswer;
                    StringAnswer = enterTxt.Text;
                    int result;
                    if (Int32.TryParse(StringAnswer, out result))
                    {
                        MyGlobals.answerCorrect[MyGlobals.j] = float.Parse(enterTxt.Text);
                        MyGlobals.j++;
                        if (MyGlobals.answer == Int32.Parse(enterTxt.Text))//is it correct//is it correct
                        {
                            correctOrNot.Text = "correct";
                            MyGlobals.score += 1;
                            scoreTtxt.Text = MyGlobals.score.ToString();
                            MyGlobals.questionsAnswered += 1;
                            enterTxt.Text = "";
                            _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                            Stream beepStream = GetType().Assembly.GetManifestResourceStream("mobileAppAssigment.correct.wav");
                            bool isSuccess = _simpleAudioPlayer.Load(beepStream);
                            _simpleAudioPlayer.Play();
                            spawnQuestions();
                        }
                        else//its incorrect
                        {
                            correctOrNot.Text = "incorrect";
                            MyGlobals.questionsAnswered += 1;
                            enterTxt.Text = "";
                            _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                            Stream beepStream = GetType().Assembly.GetManifestResourceStream("mobileAppAssigment.wrong.wav");
                            bool isSuccess = _simpleAudioPlayer.Load(beepStream);
                            _simpleAudioPlayer.Play();
                            spawnQuestions();
                        }
                    }
                    else
                    {
                        MyGlobals.answerCorrect[MyGlobals.j] = 1000f;
                        MyGlobals.j++;
                        correctOrNot.Text = "incorrect";
                        MyGlobals.questionsAnswered += 1;
                        enterTxt.Text = "";
                        _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                        Stream beepStream = GetType().Assembly.GetManifestResourceStream("mobileAppAssigment.wrong.wav");
                        bool isSuccess = _simpleAudioPlayer.Load(beepStream);
                        _simpleAudioPlayer.Play();
                        spawnQuestions();
                    }
                }
                else//there is a timer so they selected level 1 or 2
                {

                    if (enterTxt.Text != null)
                    {
                        string StringAnswer;
                        StringAnswer = enterTxt.Text;
                        int result;
                        if (Int32.TryParse(StringAnswer, out result))
                        {
                            MyGlobals.answerCorrect[MyGlobals.j] = float.Parse(enterTxt.Text);
                            MyGlobals.j++;
                            if (MyGlobals.answer == Int32.Parse(enterTxt.Text))//is it correct//is it correct
                            {
                               
                                correctOrNot.Text = "correct";
                                MyGlobals.score += 1;
                                scoreTtxt.Text = MyGlobals.score.ToString();
                                MyGlobals.questionsAnswered += 1;
                                enterTxt.Text = "";
                                _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                                Stream beepStream = GetType().Assembly.GetManifestResourceStream("mobileAppAssigment.correct.wav");
                                bool isSuccess = _simpleAudioPlayer.Load(beepStream);
                                _simpleAudioPlayer.Play();
                                spawnQuestions();
                            }
                            else//its incorrect
                            {
                                
                                correctOrNot.Text = "incorrect";
                                MyGlobals.questionsAnswered += 1;
                                enterTxt.Text = "";
                                _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                                Stream beepStream = GetType().Assembly.GetManifestResourceStream("mobileAppAssigment.wrong.wav");
                                bool isSuccess = _simpleAudioPlayer.Load(beepStream);
                                _simpleAudioPlayer.Play();
                                spawnQuestions();
                            }
                        }
                        else
                        {
                            MyGlobals.answerCorrect[MyGlobals.j] = 1000;
                            MyGlobals.j++;
                            correctOrNot.Text = "incorrect";
                            MyGlobals.questionsAnswered += 1;
                            enterTxt.Text = "";
                            _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                            Stream beepStream = GetType().Assembly.GetManifestResourceStream("mobileAppAssigment.wrong.wav");
                            bool isSuccess = _simpleAudioPlayer.Load(beepStream);
                            _simpleAudioPlayer.Play();
                            spawnQuestions();
                        }
                    }
                    else
                    {
                        
                        correctOrNot.Text = "incorrect";
                        MyGlobals.questionsAnswered += 1;
                        enterTxt.Text = "";
                        _simpleAudioPlayer = CrossSimpleAudioPlayer.CreateSimpleAudioPlayer();
                        Stream beepStream = GetType().Assembly.GetManifestResourceStream("mobileAppAssigment.wrong.wav");
                        bool isSuccess = _simpleAudioPlayer.Load(beepStream);
                        _simpleAudioPlayer.Play();
                        spawnQuestions();
                    }

                }
            }
            else
            {

                //var ans = DisplayAlert("Finished", "Well done, your score is " + MyGlobals.score + "/10", null, "Exit");

                //DisplayAlert("Finished", "Well done, your score is " + MyGlobals.score + "/10", null, "Exit");
                //Thread.CurrentThread.Abort();
               
                Navigation.PushModalAsync(new Page4(MyGlobals.score));


            }
            
            
        }
        private void spawnQuestions()
        {
            
            MyGlobals.rnd = new Random();
            MyGlobals.num1 = MyGlobals.rnd.Next(1, 13);
            MyGlobals.num2 = MyGlobals.rnd.Next(1, 13);
            MyGlobals.symbolNumber = MyGlobals.rnd.Next(1, 5);
            
            if (MyGlobals.symbolNumber == 1)
            {
                MyGlobals.symbol = "+";
                MyGlobals.answer = MyGlobals.num1 + MyGlobals.num2;
            }
            else if (MyGlobals.symbolNumber == 2)
            {
                MyGlobals.symbol = "-";
                MyGlobals.answer = MyGlobals.num1 - MyGlobals.num2;
            }
            else if (MyGlobals.symbolNumber == 3)
            {
                MyGlobals.symbol = "x";
                MyGlobals.answer = MyGlobals.num1 * MyGlobals.num2;
            }
            else if (MyGlobals.symbolNumber == 4)
            {
                MyGlobals.symbol = "÷";
                MyGlobals.answer = MyGlobals.num1 / MyGlobals.num2;
                
            }
            
            questionTxt.Text = MyGlobals.num1 + MyGlobals.symbol + MyGlobals.num2;
            
            if (MyGlobals.answer > 0)//if answer is less than 0 OR answer not a whole number
            {
                if(MyGlobals.answer % 1 != 0)
                {
                    spawnQuestions();
                }
                else if (MyGlobals.answer == 0.5)
                {
                    MyGlobals.answers[MyGlobals.i] = MyGlobals.answer;
                    
                    
                    MyGlobals.i++;
                }

                MyGlobals.answers[MyGlobals.i] = MyGlobals.answer;
                
                
                MyGlobals.i++;
            }
            //else if (MyGlobals.answer == 0.5)
            //{
            //    //nothing, answer is valid
            //}
            else
            {
                //callQuestions();
                spawnQuestions();
            }
            
        }

        private void callQuestions()
        {
            spawnQuestions();
        }


    }
}