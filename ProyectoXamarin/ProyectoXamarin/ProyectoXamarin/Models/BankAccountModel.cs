using Realms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoXamarin.Models
{
    public class BankAccountModel : RealmObject
    {
        public string Name { get; set; }

        public async static Task<IEnumerable<BankAccountModel>> GetAllAccount() 
        {
            try
            {
                Realm realm = Realm.GetInstance();

                System.Linq.IQueryable<BankAccountModel> accounts = realm.All<BankAccountModel>();

                return accounts;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static async Task<BankAccountModel> GetAccount(string name)
        {
            try
            {
                Realm realm = Realm.GetInstance();

                BankAccountModel account = realm.All<BankAccountModel>().Where(x => x.Name == name).FirstOrDefault();

                return account;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
