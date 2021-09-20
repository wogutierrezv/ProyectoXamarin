using Realms;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoXamarin.Models
{
    public class ExpenseModel : RealmObject
    {
        public decimal Monto { get; set; }
        public string Descripcion { get; set; }
        public BankAccountModel Cuenta { get; set; }
        public CategoryModel Categoria { get; set; }
        public string Fecha { get; set; }
        public string Adjunto { get; set; }

        public async static Task<IEnumerable<ExpenseModel>> GetAllExpense()
        {
            try
            {
                Realm realm = Realm.GetInstance();

                System.Linq.IQueryable<ExpenseModel> expenses = realm.All<ExpenseModel>();

                return expenses;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
