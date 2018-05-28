using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermPaper.Models
{
    public class CurrentUserModel
    {
        public string Id { get; set; }

        public ICollection<LotModel> Lots { get; set; }

        public ICollection<BiddingModel> Biddings { get; set; }
    }
}