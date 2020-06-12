using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            DonateDetail = new HashSet<DonateDetail>();
            LikeDetail = new HashSet<LikeDetail>();
        }

        public string CampaignName { get; set; }
        public double Amount { get; set; }
        public double CurrentlyMoney { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CategoryId { get; set; }
        public int? AuthorId { get; set; }
        public int Id { get; set; }
        public int? Popular { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<DonateDetail> DonateDetail { get; set; }
        public virtual ICollection<LikeDetail> LikeDetail { get; set; }
    }
}
