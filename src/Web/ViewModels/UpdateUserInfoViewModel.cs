using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class UpdateUserInfoViewModel
    {
        public int Id { get; set; } // сделать маппинг!!!!!!!!!!!!!!!!!!
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Sex { get; set; } // 1 - woman, 0 - man
        public string BirthDate { get; set; }
        public string Photo100 { get; set; }
        public string Photo200 { get; set; }
    }
}
