using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using Microcharts;
using SkiaSharp;
using ProyectoXamarin.ViewModels;
using ProyectoXamarin.Models;

namespace ProyectoXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphicView : ContentPage
    {
        List<ChartEntry> entryList;
        public GraphicView()
        {
            InitializeComponent();
            entryList = new List<ChartEntry>();
            LoadEntries();
            donutChar.Chart = new DonutChart()
            {
                Entries = entryList,
                LabelTextSize = 45,
                AnimationDuration = new TimeSpan(16000)
            };
        }

        private void LoadEntries()
        {
            IList<ExpenseModel> expenseModels = HomeViewModel.GetInstance().lstExpenseModel;

            var query = expenseModels
                .GroupBy(x => x.Categoria.Name)
                .Select(sl => new ExpenseModel()
                {
                    Monto = sl.Sum(c => c.Monto),
                    Descripcion = sl.First().Descripcion,
                    Categoria = sl.First().Categoria
                });

            Random random = new Random();

            foreach (var item in query)
            {
                string colorHex = $"#{random.Next(0x1000000):X6}";

                SKColor color = SKColor.Parse(colorHex);

                ChartEntry chartEntry = new ChartEntry((float)item.Monto)
                {
                    Label = item.Categoria.Name,
                    ValueLabel = item.Monto.ToString(),
                    Color = color,
                    ValueLabelColor = color,
                    TextColor = SKColor.Parse("#000000")
                };

                entryList.Add(chartEntry);
            }
        }
    }
}