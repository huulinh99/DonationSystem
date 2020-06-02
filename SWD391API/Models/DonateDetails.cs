using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class DonateDetails
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public string UserId { get; set; }
        public double? Amount { get; set; }
        public DateTime? Date { get; set; }
        public string Description { get; set; }
        public int? GiftId { get; set; }

        public virtual Campaigns Campaign { get; set; }
        public virtual GiftDetails Gift { get; set; }
        public virtual Users User { get; set; }
    }
}
