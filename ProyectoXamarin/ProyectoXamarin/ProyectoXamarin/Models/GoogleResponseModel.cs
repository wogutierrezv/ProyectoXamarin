using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoXamarin.Models
{
    public class GoogleResponseModel
    {
        public string sub { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string picture { get; set; }
        public string email { get; set; }
    }
}
