using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class Campaigns
    {
        public Campaigns()
        {
            Carelesses = new HashSet<Carelesses>();
            DonateDetails = new HashSet<DonateDetails>();
            GiftDetails = new HashSet<GiftDetails>();
        }

        public int CampaignId { get; set; }
        public string CampaignName { get; set; }
        public double Amount { get; set; }
        public double CurrentlyMoney { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string UserId { get; set; }
        public int Popular { get; set; }
        public string Description { get; set; }
        public bool Approved { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CategoryId { get; set; }

        public virtual Categories Category { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Carelesses> Carelesses { get; set; }
        public virtual ICollection<DonateDetails> DonateDetails { get; set; }
        public virtual ICollection<GiftDetails> GiftDetails { get; set; }
    }
}
