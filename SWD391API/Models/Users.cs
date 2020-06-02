using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class Users
    {
        public Users()
        {
            Campaigns = new HashSet<Campaigns>();
            Carelesses = new HashSet<Carelesses>();
            DonateDetails = new HashSet<DonateDetails>();
        }

        public string UserId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public double Balance { get; set; }
        public int? LoginMethodId { get; set; }

        public virtual LoginMethods LoginMethod { get; set; }
        public virtual UserRoles Role { get; set; }
        public virtual ICollection<Campaigns> Campaigns { get; set; }
        public virtual ICollection<Carelesses> Carelesses { get; set; }
        public virtual ICollection<DonateDetails> DonateDetails { get; set; }
    }
}
