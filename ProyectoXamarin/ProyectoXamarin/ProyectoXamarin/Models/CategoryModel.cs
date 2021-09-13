using Realms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoXamarin.Models
{
    public class CategoryModel : RealmObject
    {
        public string Name { get; set; }

        public async static Task<IEnumerable<CategoryModel>> GetAllCategory()
        {
            try
            {
                Realm realm = Realm.GetInstance();

                System.Linq.IQueryable<CategoryModel> accounts = realm.All<CategoryModel>();

                return accounts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<CategoryModel> GetCategory(string name)
        {
            try
            {
                Realm realm = Realm.GetInstance();

                CategoryModel account = realm.All<CategoryModel>().Where(x => x.Name == name).FirstOrDefault();

                return account;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
