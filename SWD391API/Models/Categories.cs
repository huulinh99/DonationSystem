using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class Categories
    {
        public Categories()
        {
            Campaigns = new HashSet<Campaigns>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Campaigns> Campaigns { get; set; }
    }
}
