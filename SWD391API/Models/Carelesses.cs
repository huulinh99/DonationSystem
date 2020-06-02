using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class Carelesses
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int CampaignId { get; set; }
        public DateTime? Date { get; set; }
        public int? Count { get; set; }

        public virtual Campaigns Campaign { get; set; }
        public virtual Users User { get; set; }
    }
}
