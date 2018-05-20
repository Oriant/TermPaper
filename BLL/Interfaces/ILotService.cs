﻿using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	public interface ILotService
	{
		void CreateLot(LotDTO lotDTO);
		IEnumerable<LotDTO> GetLots();
		void Dispose();
	}
}
