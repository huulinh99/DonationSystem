using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SWD391API.Paging
{
    public class GetAllPostsQuery
    {
        [FromQuery(Name = "campaignName")]
        public string CampaignName { get; set; }
    }
}
