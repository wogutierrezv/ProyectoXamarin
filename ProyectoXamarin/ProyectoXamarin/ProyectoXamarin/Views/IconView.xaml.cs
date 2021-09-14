using ProyectoXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IconView : ContentPage
    {
        public IconView()
        {
            InitializeComponent();
            BindingContext = CategoryViewModel.GetInstance();
        }

        async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Label label = sender as Label;

            if (label != null)
            {

            }

            await Navigation.PopModalAsync(true);
        }
    }
}