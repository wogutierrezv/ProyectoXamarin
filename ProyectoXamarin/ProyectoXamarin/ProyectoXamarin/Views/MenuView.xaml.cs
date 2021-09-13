using ProyectoXamarin.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuView : ContentPage
    {
        public MenuView(LoginViewModel loginViewModel)
        {
            InitializeComponent();
            BindingContext = new MenuViewModel();
            stlUser.BindingContext = loginViewModel;
        }
    }
}