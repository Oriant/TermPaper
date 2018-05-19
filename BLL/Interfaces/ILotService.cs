using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
	interface ILotService
	{
		void CreateLot(LotDTO lotDTO);
		CategoryDTO GetCategory(int? id);
		IEnumerable<CategoryDTO> GetCategories();
		IEnumerable<LotDTO> GetLots();
		void Dispose();
	}
}
