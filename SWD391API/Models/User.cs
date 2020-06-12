using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class User
    {
        public User()
        {
            DonateDetail = new HashSet<DonateDetail>();
            LikeDetail = new HashSet<LikeDetail>();
        }

        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public double Balance { get; set; }
        public int? LoginMethodId { get; set; }
        public int Id { get; set; }

        public virtual UserRole Role { get; set; }
        public virtual ICollection<DonateDetail> DonateDetail { get; set; }
        public virtual ICollection<LikeDetail> LikeDetail { get; set; }
    }
}
