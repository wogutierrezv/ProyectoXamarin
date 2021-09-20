using ProyectoXamarin.Models;
using ProyectoXamarin.ViewModels;
using ProyectoXamarin.Views;
using Realms;
using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoXamarin
{
    public partial class App : Application
    {
        [Obsolete]
        public App()
        {
            try
            {
                InitializeComponent();

                string rememberUser = Preferences.Get("rememberUser", "N");
                string loginWithGoogle = Preferences.Get("loginWithGoogle", "N");

                if (loginWithGoogle == "S")
                {
                    string userToken = Preferences.Get("UserToken", null);

                    LoginViewModel loginViewModel = new LoginViewModel();
                    loginViewModel.GetUserInfoUsingToken(userToken).ConfigureAwait(false);

                    NavigationPage navigationPage = new NavigationPage(new HomeView());

                    MainPage = new MasterDetailPage
                    {
                        Master = new MenuView(loginViewModel),
                        Detail = navigationPage
                    };
                }
                else 
                {
                    if (rememberUser == "S")
                    {
                        string email = Preferences.Get("rememberEmail", null);

                        Realm reaml = Realm.GetInstance();
                        UserModel dbUser = reaml.All<UserModel>().Where(x => x.Email == email).FirstOrDefault();

                        LoginViewModel loginViewModel = new LoginViewModel
                        {
                            User = dbUser
                        };

                        NavigationPage navigationPage = new NavigationPage(new HomeView());

                        MainPage = new MasterDetailPage
                        {
                            Master = new MenuView(loginViewModel),
                            Detail = navigationPage
                        };
                    }
                    else { MainPage = new LoginView(); }
                }



                //MainPage = new IconView();
            }
            catch (Exception ex)
            {
                //Current.MainPage.DisplayAlert("Error", $"Login error: {ex.Message}", "Ok");
            }
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
