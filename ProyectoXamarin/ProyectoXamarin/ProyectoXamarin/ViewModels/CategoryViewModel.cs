using ProyectoXamarin.Models;
using ProyectoXamarin.Views;
using Realms;
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
        public ICommand EnterIconCommand { get; set; }
        public ICommand SelectedIconCommand { get; set; }

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
            EnterIconCommand = new Command(EnterIcon);
            SelectedIconCommand = new Command<string>(SelectedIcon);
        }

        private void SelectedIcon(string obj)
        {
            Realm realm = Realm.GetInstance();

            CurrentCategory.Icon = obj;

            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PopModalAsync(true);
        }

        private void EnterIcon()
        {
            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushModalAsync(new IconView(), true);
        }

        private async void EnterDelete(string obj)
        {
            bool answer = await Application.Current.MainPage.DisplayAlert("Confirmar eliminación", "Desea eliminar el registro?", "Sí", "No");

            if (answer)
            {
                Realm realm = Realm.GetInstance();
                var account = CategoryModel.GetCategory(obj).Result;
                using (var trans = realm.BeginWrite())
                {
                    realm.Remove(account);
                    trans.Commit();
                }

                InitClass();
            }
        }

        async void EnterRegister(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(CurrentCategory.Name))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Debe ingresar un nombre para la categoría", "Ok");
                    return;
                }

                Realm realm = Realm.GetInstance();

                if (isNew)
                {
                    realm.Write(() =>
                    {
                        realm.Add(CurrentCategory);
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
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        private void EnterDetailCommand(string obj)
        {
            if (!string.IsNullOrEmpty(obj))
            {
                CurrentCategory = CategoryModel.GetCategory(obj).Result;
                isNew = false;
            }
            else { CurrentCategory = new CategoryModel() { Icon = "" }; isNew = true; }

            ((MasterDetailPage)Application.Current.MainPage).Detail.Navigation.PushAsync(new DetailCategoryView());
        }
    }
}
