using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class BiddingDTO
    {
        public int Id { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public int LotId { get; set; }
    }
}
