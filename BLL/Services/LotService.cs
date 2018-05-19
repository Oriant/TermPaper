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
				UserId = lotDTO.UserId
				
			};

			Database.Lots.Create(lot);
			Database.Save();
		}

		public CategoryDTO GetCategory(int? id)
		{
			if (id == null)
				throw new ValidationException("Category ID undefined");

			var category = Database.Categories.Get(id.Value);

			if (category == null)
				throw new ValidationException("Category not found");

			return new CategoryDTO {  Id = category.Id, Name = category.Name };
		}

		public IEnumerable<CategoryDTO> GetCategories()
		{
			var mapper = new MapperConfiguration(cfg => 
			cfg.CreateMap<Category, CategoryDTO>()).CreateMapper();

			return mapper.Map<IEnumerable<Category>, List<CategoryDTO>>(Database.Categories.GetAll());
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
			throw new NotImplementedException();
		}

		
	}
}
