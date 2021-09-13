using ProyectoXamarin.Views;
using System;
using System.Windows.Input;
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
                case "3":
                    await ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new CategoryView());
                    ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                    break;
                default:
                    break;
            }
        }
    }
}
