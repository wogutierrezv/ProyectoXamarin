using ProyectoXamarin.Models;
using ProyectoXamarin.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProyectoXamarin.ViewModels
{
    public class CategoryViewModel : BaseViewModel
    {
        #region Properties

        private bool isNew = false;

        private IEnumerable<CategoryModel> _lstCategoryModel;

        public IEnumerable<CategoryModel> lstCategoryModel
        {
            get
            {
                return _lstCategoryModel;
            }
            set
            {
                _lstCategoryModel = value;
                OnPropertyChanged(nameof(lstCategoryModel));
            }
        }

        private CategoryModel _CurrentCategory = new CategoryModel();

        public CategoryModel CurrentCategory
        {
            get
            {
                return _CurrentCategory;
            }
            set
            {
                _CurrentCategory = value;
                OnPropertyChanged(nameof(CurrentCategory));
            }
        }

        #endregion Properties

        #region Command

        public ICommand EnterCategoryDetailCommand { get; set; }
        public ICommand EnterRegisterCommand { get; set; }
        public ICommand EnterDeleteCommand { get; set; }

        #endregion Command

        #region Singlenton

        private static CategoryViewModel instance = null;

        private CategoryViewModel()
        {
            InitClass();
            InitCommand();
        }

        public static CategoryViewModel GetInstance()
        {
            if (instance == null)
                instance = new CategoryViewModel();

            return instance;
        }

        #endregion Singlenton

        public async void InitClass()
        {
            lstCategoryModel = await CategoryModel.GetAllCategory();
        }

        public void InitCommand()
        {
            EnterCategoryDetailCommand = new Command<string>(EnterDetailCommand);
            EnterRegisterCommand = new Command(EnterRegister);
            EnterDeleteCommand = new Command<string>(EnterDelete);
        }

        private void EnterDelete(string obj)
        {
            throw new NotImplementedException();
        }

        private void EnterRegister(object obj)
        {
            throw new NotImplementedException();
        }

        private void EnterDetailCommand(string obj)
        {
            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetailCategoryView());
        }
    }
}
