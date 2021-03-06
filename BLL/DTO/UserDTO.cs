﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTO
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<LotDTO> Lots { get; set; }
        public ICollection<BidDTO> Biddings { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
    }
}
