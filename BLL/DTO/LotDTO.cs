using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class LotDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal StartPrice { get; set; }

        public decimal CurrentPrice { get; set; }

        public decimal BidRate { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? FinishDate { get; set; }

        public bool IsFinished { get; set; }

        public bool IsConfirmed { get; set; }


        public ICollection<BiddingDTO> Biddings { get; set; }

        public CategoryDTO Category { get; set; }

        public string UserId { get; set; }
    }
}
