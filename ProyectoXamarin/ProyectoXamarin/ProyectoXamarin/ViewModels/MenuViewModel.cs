using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class MenuViewModel : INotifyPropertyChanged
    {
        #region Command

        public ICommand EnterMenuOptionCommand { get; set; }

        #endregion Command

        public MenuViewModel() 
        {
            EnterMenuOptionCommand = new Command(EnterMenuOption);
        }

        private void EnterMenuOption()
        {
            string d = "d";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
