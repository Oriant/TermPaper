using AutoMapper;
using BLL.DTO;
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

        public void ConfirmLot(LotDTO lotDto)
        {
            lotDto.IsConfirmed = true;
            var lot = Mapper.Map<Lot>(lotDto);

            Database.Lots.Update(lot);
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
