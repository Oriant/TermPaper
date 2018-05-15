using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entities
{
    public class Lot
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public bool IsConfirmed { get; set; }

        public virtual Category Category { get; set; }
		public int CategoryId { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }

        
    }
}
