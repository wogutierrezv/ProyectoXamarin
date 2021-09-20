using ProyectoXamarin.Models;
using ProyectoXamarin.Views;
using Realms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
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
        public ICommand EnterFilePickerCommand { get; set; }
        public ICommand EnterNewExpenseCommand { get; set; }
        public ICommand EnterDeleteCommand { get; set; }
        public ICommand PerformSearchCommand { get; set; }
        public ICommand EnterExpenseDetailCommand { get; set; }

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
        }

        public void InitCommand()
        {
            EnterRegisterCommand = new Command(EnterRegister);
            EnterFilePickerCommand = new Command(EnterFilePicker);
            EnterNewExpenseCommand = new Command(EnterNewExpense);
            EnterDeleteCommand = new Command<ExpenseModel>(EnterDeleteExpense);
            PerformSearchCommand = new Command<string>(PerformSearch);
            EnterExpenseDetailCommand = new Command<ExpenseModel>(EnterExpenseDetail);
        }

        private void EnterExpenseDetail(ExpenseModel expenseModel)
        {
            CurrentExpense = expenseModel;
            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetailExpenseView());
        }

        private void PerformSearch(string search)
        {
            try
            {
                var normalizedQuery = search?.ToLower() ?? "";

                lstExpenseModel = ExpenseModel.GetAllExpense().Result.Where(f => f.Descripcion.ToLowerInvariant().Contains(normalizedQuery)).ToList();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void EnterDeleteExpense(ExpenseModel expenseModel)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Confirmar eliminación", "Desea eliminar el registro?", "Sí", "No");

            if (answer)
            {
                Realm realm = Realm.GetInstance();
                //var account = CategoryModel.GetCategory(obj).Result;
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(expenseModel);
                    trans.Commit();
                }

                InitClass();
            }
        }

        public async void InitMaintenance() 
        {
            lstBankAccountModel = BankAccountModel.GetAllAccount().Result.ToList();
            lstCategoryModel = CategoryModel.GetAllCategory().Result.ToList();
        }

        private void EnterNewExpense()
        {
            InitMaintenance();
            CurrentExpense = new ExpenseModel() { Fecha = DateTime.Today.Date.ToString() };
            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new ExpenseView());
        }

        private async void EnterFilePicker(object obj)
        {
            var result = await FilePicker.PickAsync();

            if (result != null)
            {
                CurrentExpense.Adjunto = result.FullPath;
            }
        }

        private async void EnterRegister()
        {
            try
            {
                if (CurrentExpense.Monto <= decimal.Zero)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El monto ingresado debe ser mayor a 0", "Ok");
                    return;
                }

                if (string.IsNullOrEmpty(CurrentExpense.Descripcion))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar una descripción para el gasto", "Ok");
                    return;
                }

                if (CurrentExpense.Cuenta == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar una cuenta para el gasto", "Ok");
                    return;
                }

                if (CurrentExpense.Categoria == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar una categoría para el gasto", "Ok");
                    return;
                }

                Realm realm = Realm.GetInstance();

                realm.Write(() =>
                {
                    realm.Add(CurrentExpense);
                });

                lstExpenseModel = ExpenseModel.GetAllExpense().Result.ToList();

                ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }
    }
}
