using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class Author
    {
        public Author()
        {
            Campaign = new HashSet<Campaign>();
        }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string RoleId { get; set; }
        public int Id { get; set; }
        public int? Count { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}
