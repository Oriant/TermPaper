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

        public decimal Price { get; set; }

        public bool IsConfirmed { get; set; }

        public Category Category { get; set; }
		public int CategoryId { get; set; }

		public string UserId { get; set; }

		//public UserDTO User { get; set; }

        
    }
}
