﻿using ProyectoXamarin.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseView : ContentPage
    {
        public ExpenseView()
        {
            InitializeComponent();
            BindingContext = HomeViewModel.GetInstance();
        }
    }
}