using Realms;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class UserModel : RealmObject
    {
        public string Name { get; set; }
        [PrimaryKey]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Picture { get; set; }
        public bool RememberUser { get; set; }
    }
}
