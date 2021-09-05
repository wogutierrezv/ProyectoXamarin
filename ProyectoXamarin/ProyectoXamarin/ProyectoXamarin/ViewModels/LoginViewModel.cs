using Newtonsoft.Json;
using ProyectoXamarin.Models;
using ProyectoXamarin.Views;
using Realms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        const string authenticationUrl = "https://localhost:44300/mobileauth/";
        private JsonSerializer _serializer = new JsonSerializer();

        #region Properties

        private UserModel _User = new UserModel();

        public UserModel User
        {
            get
            {
                return _User;
            }
            set
            {
                _User = value;
                OnPropertyChanged("User");
            }
        }

        #endregion Properties

        #region Command

        public ICommand GoogleCommand { get; }
        public ICommand LoginCommand { get; set; }
        public ICommand EnterRegisterCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        #endregion Command

        string accessToken = string.Empty;

        public string AuthToken { get; set; }

        public LoginViewModel() 
        {
            LoginCommand = new Command(LoginAsync);
            GoogleCommand = new Command(async () => await OnAuthenticate("Google"));
            EnterRegisterCommand = new Command(EnterRegister);
            RegisterCommand = new Command(Register);
        }

        public async void Register(object obj)
        {
            try
            {
                var realm = Realm.GetInstance();

                var users = realm.All<UserModel>().Where(x => x.Email == User.Email).FirstOrDefault();

                if (users != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El correo ingresado ya existe en la base de datos", "Ok");
                    return;
                }

                realm.Write(() =>
                {
                    _ = realm.Add(User);
                });

                await Application.Current.MainPage.DisplayAlert("Info", "Usuario creado correctamente", "Ok");
                Application.Current.MainPage = new LoginView();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Login error: {ex.Message}", "Ok");
            }
        }

        public async void EnterRegister(object obj)
        {
            Application.Current.MainPage = new RegisterView();
        }

        public async void LoginAsync()
        {
            try
            {
                var reaml = Realm.GetInstance();

                var dbUser = reaml.All<UserModel>().Where(x => x.Email == User.Email).FirstOrDefault();

                if (dbUser == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "El correo ingresado no existe.", "Ok");
                    return;
                }

                if (dbUser.Password != User.Password)
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "Credenciales incorrectas.", "Ok");
                    return;
                }

                if (User.RememberUser)
                {
                    Preferences.Set("rememberUser", "S");
                }
                else { Preferences.Set("rememberUser", "N"); }

                NavigationPage navigationPage = new NavigationPage(new HomeView());

                Application.Current.MainPage = new MasterDetailPage
                {
                    Master = new MenuView(this),
                    Detail = navigationPage
                };
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Login error: {ex.Message}", "Ok");
            }
        }

        async Task OnAuthenticate(string scheme)
        {
            try
            {
                WebAuthenticatorResult r = null;

                if (scheme.Equals("Apple")
                    && DeviceInfo.Platform == DevicePlatform.iOS
                    && DeviceInfo.Version.Major >= 13)
                {
                    r = await AppleSignInAuthenticator.AuthenticateAsync();
                }
                else if (scheme.Equals("Google"))
                {
                    var authUrl = new Uri(authenticationUrl + scheme);
                    var callbackUrl = new Uri("xamarinessentials://");

                    r = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);
                }

                AuthToken = r?.AccessToken ?? r?.IdToken;
                GetUserInfoUsingToken(AuthToken);
            }
            catch (Exception ex)
            {
                AuthToken = string.Empty;
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }
        }

        private async void GetUserInfoUsingToken(string authToken)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.googleapis.com/oauth2/v3/");
            var httpResponseMessage = await httpClient.GetAsync("tokeninfo?access_token=" + authToken);
            using (var stream = await httpResponseMessage.Content.ReadAsStreamAsync())
            using (var reader = new StreamReader(stream))
            using (var json = new JsonTextReader(reader))
            {
                var jsoncontent = _serializer.Deserialize<GoogleResponseModel>(json);
                Preferences.Set("UserToken", authToken);
                //Not  a best way to save auth token and check if authtoken has expired insted try implementing refresh token
                //await Navigation.PushAsync(new MyDashboardPage(jsoncontent.email));
            }

        }

        #region PropertyChangedImplementation

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (propertyName != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
