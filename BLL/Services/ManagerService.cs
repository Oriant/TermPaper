using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ManagerService : IManagerService
    {
        IUnitOfWork Database { get; set; }

        public ManagerService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public void ConfirmLot(int id)
        {
            Lot lot = Database.Lots.Get(id);

            if (lot == null)
                throw new ValidationException("Lot not found");

            lot.IsConfirmed = true;

            Database.Lots.Update(lot);
            Database.Save();
        }

        public void DeclineLot(int id)
        {
            if (Database.Lots.Get(id) == null)
                throw new ValidationException("Lot not found");
            else
            {
                Database.Lots.Delete(id);
                Database.Save();
            }
        }

        public IEnumerable<LotDTO> GetUnconfirmedLots()
        {
            return Mapper.Map<IEnumerable<Lot>, IEnumerable<LotDTO>>
                (Database.Lots
                .GetAll()
                .Where(x => !x.IsConfirmed));
        }
    }
}
