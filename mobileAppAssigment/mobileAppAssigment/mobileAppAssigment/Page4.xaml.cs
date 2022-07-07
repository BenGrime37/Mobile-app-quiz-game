using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace mobileAppAssigment
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page4 : ContentPage
    {
        public static class MyGlobals
        {
            public static float[] answers = new float[20];
            public static bool[] answersCorrect = new bool[20];
        }
        public Page4(int score)
        {
            InitializeComponent();
            
            scoreLbl.Text = "you scored " + score + "/10";
            //scoreLbl.Text = answers[1].ToString();
        }

        private void exitBtn_Clicked(object sender, EventArgs e)
        {
            Thread.CurrentThread.Abort();
        }

        //private void answersBtn_Clicked(object sender, EventArgs e)
        //{
        //    Navigation.PushModalAsync(new Page5());
        //}
    }
}