using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class LoginMethods
    {
        public LoginMethods()
        {
            Users = new HashSet<Users>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Users> Users { get; set; }
    }
}
