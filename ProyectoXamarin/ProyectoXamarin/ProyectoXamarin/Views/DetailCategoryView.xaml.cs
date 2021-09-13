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
    public partial class DetailCategoryView : ContentPage
    {
        public DetailCategoryView()
        {
            InitializeComponent();
        }

        async void btnIcono_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new IconView(), true);
        }
    }
}