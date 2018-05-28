using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermPaper.Models
{
    public class LotsListingModel
    {
        public IEnumerable<LotModel> PendingLots { get; set; }
        public IEnumerable<LotModel> ActiveLots { get; set; }
        public IEnumerable<LotModel> SoldLots { get; set; }
        public IEnumerable<LotModel> PurchasedLots { get; set; }
        public IEnumerable<LotModel> IgnoredLots { get; set; }
    }
}