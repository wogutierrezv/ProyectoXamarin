﻿using ProyectoXamarin.ViewModels;
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
    public partial class DetailExpenseView : ContentPage
    {
        public DetailExpenseView()
        {
            InitializeComponent();
            BindingContext = HomeViewModel.GetInstance();
        }
    }
}