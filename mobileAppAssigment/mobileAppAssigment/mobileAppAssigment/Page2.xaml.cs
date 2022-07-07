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
    public partial class Page2 : ContentPage
    {
        public Page2()
        {
            InitializeComponent();

        }
        //can go back to main page but crashes when it goes to any other page
        private void lvl0_Clicked(object sender, EventArgs e)
        {
            int level = 0;
            Navigation.PushAsync(new NavigationPage(new Page3(level)));
            //Navigation.PushAsync(new MainPage());


        }

        async void lvl1_Clicked(object sender, EventArgs e)
        {
            int level = 1;
            await Navigation.PushModalAsync (new Page3(level));
            
        }

        private void lvl2_Clicked(object sender, EventArgs e)
        {
            int level = 2;
            Navigation.PushAsync(new NavigationPage(new Page3(level)));
        }
    }
}