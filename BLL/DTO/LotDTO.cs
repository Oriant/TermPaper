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

        public ICollection<CategoryDTO> Categories { get; set; }

        public UserDTO User { get; set; }

        public LotDTO()
        {
            Categories = new List<CategoryDTO>();
            IsConfirmed = false;
        }
    }
}
