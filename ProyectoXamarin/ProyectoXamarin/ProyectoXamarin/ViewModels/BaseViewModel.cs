using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ProyectoXamarin.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties

        private bool _isBusy;

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        #endregion Properties

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
