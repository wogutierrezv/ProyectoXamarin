using ProyectoXamarin.Models;
using ProyectoXamarin.Views;
using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class BankAccountViewModel : BaseViewModel
    {
        #region Properties

        private bool isNew = false;

        private IEnumerable<BankAccountModel> _lstBankAccountModel;

        public IEnumerable<BankAccountModel> lstBankAccountModel
        {
            get
            {
                return _lstBankAccountModel;
            }
            set
            {
                _lstBankAccountModel = value;
                OnPropertyChanged(nameof(lstBankAccountModel));
            }
        }

        private BankAccountModel _CurrentBankAccount = new BankAccountModel();

        public BankAccountModel CurrentBankAccount
        {
            get
            {
                return _CurrentBankAccount;
            }
            set
            {
                _CurrentBankAccount = value;
                OnPropertyChanged(nameof(CurrentBankAccount));
            }
        }

        #endregion Properties

        #region Command

        public ICommand EnterAccountDetailCommand { get; set; }
        public ICommand EnterRegisterCommand { get; set; }
        public ICommand EnterDeleteCommand { get; set; }

        #endregion Command

        #region Singlenton

        private static BankAccountViewModel instance = null;

        private BankAccountViewModel()
        {
            InitClass();
            InitCommand();
        }

        public static BankAccountViewModel GetInstance()
        {
            if (instance == null)
                instance = new BankAccountViewModel();

            return instance;
        }

        #endregion Singlenton

        public async void InitClass()
        {
            lstBankAccountModel = await BankAccountModel.GetAllAccount();
        }

        public void InitCommand()
        {
            EnterAccountDetailCommand = new Command<string>(EnterDetailCommand);
            EnterRegisterCommand = new Command(EnterRegister);
            EnterDeleteCommand = new Command<string>(EnterDelete);
        }

        private async void EnterDelete(string obj)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Confirmar eliminación", "Desea eliminar el registro?", "Sí", "No");

            if (answer)
            {
                Realm realm = Realm.GetInstance();
                var account = BankAccountModel.GetAccount(obj).Result;
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(account);
                    trans.Commit();
                }

                InitClass();
            }
        }

        private void EnterRegister()
        {
            Realm realm = Realm.GetInstance();

            if (isNew)
            {
                realm.Write(() =>
                {
                    realm.Add(CurrentBankAccount);
                });
            }
            else 
            {
                using (var trans = realm.BeginWrite())
                {
                    trans.Commit();
                }
            }

            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PopAsync();
        }

        private void EnterDetailCommand(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                CurrentBankAccount = BankAccountModel.GetAccount(obj).Result;
                isNew = false;
            }
            else { CurrentBankAccount = new BankAccountModel(); isNew = true; }

            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetailAccountView());
        }
    }
}
