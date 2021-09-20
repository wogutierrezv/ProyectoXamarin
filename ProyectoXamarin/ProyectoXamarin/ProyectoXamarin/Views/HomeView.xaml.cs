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
    public partial class HomeView : ContentPage
    {
        public HomeView()
        {
            InitializeComponent();
            BindingContext = HomeViewModel.GetInstance();
        }

        private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var normalizedQuery = e.NewTextValue?.ToLower() ?? "";

            if (string.IsNullOrEmpty(normalizedQuery))
            {
                listExpenses.ItemsSource = HomeViewModel.GetInstance().lstExpenseModel;
                return;
            }

            listExpenses.ItemsSource = HomeViewModel.GetInstance().lstExpenseModel.Where(f => f.Descripcion.ToLowerInvariant().Contains(normalizedQuery)).ToList();
        }
    }
}