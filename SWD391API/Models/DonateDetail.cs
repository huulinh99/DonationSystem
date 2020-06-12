using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class DonateDetail
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public int? UserId { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public int? GiftId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual GiftDetail Gift { get; set; }
        public virtual User User { get; set; }
    }
}
