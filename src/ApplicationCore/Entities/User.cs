using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.Entities
{
    public class User
    {
        public int VkontakteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public bool Sex { get; set; } // 1 - woman, 0 - man
        public DateTime? BirthDate { get; set; }
        public string Photo100 { get; set; }
        public string Photo200 { get; set; }    

        public ICollection<Post> Posts { get; set; }
    }
}
