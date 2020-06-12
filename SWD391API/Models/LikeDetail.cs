using System;
using System.Collections.Generic;

namespace SWD391API.Models
{
    public partial class LikeDetail
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? CampaignId { get; set; }

        public virtual Campaign Campaign { get; set; }
        public virtual User User { get; set; }
    }
}
