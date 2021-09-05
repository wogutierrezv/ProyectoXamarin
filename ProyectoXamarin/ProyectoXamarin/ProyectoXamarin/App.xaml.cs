using ProyectoXamarin.Views;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var rememberUser = Preferences.Get("rememberUser", "N");


            MainPage = new LoginView();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
