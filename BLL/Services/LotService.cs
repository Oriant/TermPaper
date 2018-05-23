using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Entities;
using BLL.Infrastructure;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Identity.Interfaces;
using DAL.Identity.Entities;

namespace BLL.Services
{
	public class LotService : ILotService
	{
		IUnitOfWork Database { get; set; }
        IUserService userService { get; set; }

		public LotService(IUnitOfWork unitOfWork, IUserService userService)
		{
			Database = unitOfWork;
            this.userService= userService;
		}

		public void CreateLot(LotDTO lotDTO)
		{
            Category category = Database.Categories.Get(lotDTO.Category.Id);
            UserDTO userDTO = userService.GetUserById(lotDTO.UserId);

            if (category == null || userDTO == null)
                throw new Infrastructure.ValidationException("Category or user doesn't exist", "");

            Lot lot = new Lot
            {
                Name = lotDTO.Name,
                Description = lotDTO.Description,
                Price = lotDTO.Price,
                Category = Database.Categories.Get(lotDTO.Category.Id),
                User = Database.Users.Get(lotDTO.UserId)
			};

			Database.Lots.Create(lot);
			Database.Save();
		}

		public IEnumerable<LotDTO> GetLots()
		{

            return Mapper.Map<IEnumerable<Lot>, List<LotDTO>>(Database.Lots.GetAll());
		}

        public LotDTO GetLotById(int id)
        {
            var lot = Database.Lots.Get(id);

            if (lot != null)
                return Mapper.Map<Lot, LotDTO>(lot);
            else
                return null;
        }

        public void Edit(LotDTO lotDTO)
        {
            var lot = Database.Lots.Get(lotDTO.Id);

            lot.Name = lotDTO.Name;
            lot.Description = lotDTO.Description;
            lot.Price = lotDTO.Price;

            Database.Lots.Update(lot);
            Database.Save();
        }

        public void Dispose()
		{
			Database.Dispose();
		}

		
	}
}
