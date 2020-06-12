using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class Category
    {
        public Category()
        {
            Campaign = new HashSet<Campaign>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}
