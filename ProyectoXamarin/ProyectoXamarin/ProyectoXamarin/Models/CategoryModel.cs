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
        public string Icon { get; set; }

        public async static Task<IEnumerable<CategoryModel>> GetAllCategory()
        {
            try
            {
                Realm realm = Realm.GetInstance();

                System.Linq.IQueryable<CategoryModel> categorys = realm.All<CategoryModel>();

                return categorys;
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

                CategoryModel category = realm.All<CategoryModel>().Where(x => x.Name == name).FirstOrDefault();

                return category;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
