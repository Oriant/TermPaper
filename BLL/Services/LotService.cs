using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
	public class LotService : ILotService
	{
		IUnitOfWork Database { get; set; }

		public LotService(IUnitOfWork unitOfWork)
		{
			Database = unitOfWork;
		}

		public void CreateLot(LotDTO lotDTO)
		{
			Lot lot = new Lot
			{
				Name = lotDTO.Name,
				Id = lotDTO.Id,
				Description = lotDTO.Description,
				CategoryId = lotDTO.CategoryId,
				Price = lotDTO.Price,
				Category = lotDTO.Category,
				IsConfirmed = lotDTO.IsConfirmed,
				UserId = lotDTO.UserId
				
			};

			Database.Lots.Create(lot);
			Database.Save();
		}

	

		public IEnumerable<LotDTO> GetLots()
		{
			var mapper = new MapperConfiguration(cfg =>
			cfg.CreateMap<Lot, LotDTO>()).CreateMapper();

            return mapper.Map<IEnumerable<Lot>, List<LotDTO>>(Database.Lots.GetAll());
		}

        public LotDTO GetLotById(int id)
        {
            var lot = Database.Lots.Get(id);

            if (lot != null)
                return Mapper.Map<Lot, LotDTO>(lot);
            else
                return null;
        }

        public void Dispose()
		{
			Database.Dispose();
		}

		
	}
}
