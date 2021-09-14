using ProyectoXamarin.Models;
using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        #region Properties

        public IList<BankAccountModel> lstBankAccountModel { get; set; }
        public IList<CategoryModel> lstCategoryModel { get; set; }

        private IList<ExpenseModel> _lstExpenseModel;
        public IList<ExpenseModel> lstExpenseModel
        {
            get
            {
                return _lstExpenseModel;
            }
            set
            {
                _lstExpenseModel = value;
                OnPropertyChanged(nameof(lstExpenseModel));
            }
        }

        private ExpenseModel _CurrentExpense = new ExpenseModel();

        public ExpenseModel CurrentExpense
        {
            get
            {
                return _CurrentExpense;
            }
            set
            {
                _CurrentExpense = value;
                OnPropertyChanged(nameof(CurrentExpense));
            }
        }

        #endregion Propierties

        #region Command

        public ICommand EnterRegisterCommand { get; set; }

        #endregion Command

        #region Singlenton

        private static HomeViewModel instance = null;

        private HomeViewModel()
        {
            InitClass();
            InitCommand();
        }

        public static HomeViewModel GetInstance()
        {
            if (instance == null)
                instance = new HomeViewModel();

            return instance;
        }

        #endregion Singlenton

        public async void InitClass()
        {
            lstExpenseModel = ExpenseModel.GetAllExpense().Result.ToList();
            lstBankAccountModel = BankAccountModel.GetAllAccount().Result.ToList();
            lstCategoryModel = CategoryModel.GetAllCategory().Result.ToList();
        }

        public void InitCommand()
        {
            EnterRegisterCommand = new Command(EnterRegister);
        }

        private void EnterRegister()
        {
            Realm realm = Realm.GetInstance();

            realm.Write(() =>
            {
                realm.Add(CurrentExpense);
            });

            lstExpenseModel = ExpenseModel.GetAllExpense().Result.ToList();

            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PopAsync();
        }
    }
}
