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

        public LotService(IUnitOfWork unitOfWork) => Database = unitOfWork;

        public void CreateLot(LotDTO lotDTO)
		{
            Category category = Database.Categories.Get(lotDTO.Category.Id);

            if (category == null)
                throw new DataValidationException("Invalid category value", "");

            Lot lot = new Lot
            {
                Name = lotDTO.Name,
                Description = lotDTO.Description,
                StartPrice = lotDTO.StartPrice,
                CurrentPrice = lotDTO.StartPrice,
                BidRate = lotDTO.BidRate,
                Category = Database.Categories.Get(lotDTO.Category.Id),
                User = Database.Users.Get(lotDTO.UserId),
                CreatorId = lotDTO.CreatorId
			};

			Database.Lots.Create(lot);

            var user = Database.Users.Get(lot.UserId);  
            user.Lots.Add(lot);  

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
            var category = Database.Categories.Get(lotDTO.Category.Id);

            lot.Name = lotDTO.Name;
            lot.Description = lotDTO.Description;
            lot.BidRate = lotDTO.BidRate;
            lot.StartPrice = lotDTO.StartPrice;
            lot.CurrentPrice = lotDTO.CurrentPrice;
            lot.Category = category;
            //lot.CategoryId = category.Id;
            lot.CreatorId = lotDTO.CreatorId;
            lot.IsConfirmed = lotDTO.IsConfirmed;
            lot.IsFinished = lotDTO.IsFinished;

            Database.Lots.Update(lot);
            Database.Categories.Update(category);

            Database.Save();
        }

        public void Expire(int id)
        {
            Lot lot = Database.Lots.Get(id);

            if (lot == null)
                throw new DataValidationException("Lot not found", "");

            lot.IsFinished = true;
            var newOwner = lot.Biddings.Count > 0 ? Database.Users.Get(lot.Biddings.Last().UserId) : null;

            if(newOwner != null)
                newOwner.Lots.Add(lot);

            Database.Lots.Update(lot);
            Database.Save();
        }

        public void Dispose()
		{
			Database.Dispose();
		}	
	}
}
