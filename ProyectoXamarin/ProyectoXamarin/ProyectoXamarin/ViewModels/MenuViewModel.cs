using ProyectoXamarin.Views;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        #region Command

        public ICommand EnterMenuOptionCommand { get; set; }

        #endregion Command

        [Obsolete]
        public MenuViewModel()
        {
            EnterMenuOptionCommand = new Command<string>(EnterMenuOption);
        }

        [Obsolete]
        private async void EnterMenuOption(string opcionMenu)
        {
            switch (opcionMenu)
            {
                case "1":
                    await((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new BankAccountView());
                    ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                    break;
                case "2":
                    await ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new ExpenseView());
                    ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                    break;
                case "3":
                    await ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new CategoryView());
                    ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                    break;
                case "4":
                    await ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new GraphicView());
                    ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                    break;
                case "5":
                    Preferences.Remove("rememberUser");
                    Preferences.Remove("rememberEmail");
                    Preferences.Remove("loginWithGoogle");
                    Preferences.Remove("UserToken");

                    Application.Current.MainPage = new LoginView();
                    break;
                default:
                    break;
            }
        }
    }
}
