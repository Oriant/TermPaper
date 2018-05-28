using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Bidding
    {
        [Key]
        public int Id { get; set; }

        public decimal Sum { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        public int LotId { get; set; }
        public virtual Lot Lot { get; set; }
    }
}
