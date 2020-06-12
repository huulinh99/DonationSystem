using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class GiftDetail
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public string GiftName { get; set; }
        public double? Amount { get; set; }
        public string Description { get; set; }

        public virtual DonateDetail DonateDetail { get; set; }
    }
}
