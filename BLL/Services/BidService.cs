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
    public class BidService : IBidService
    {
        IUnitOfWork Database { get; set; }

        public BidService(IUnitOfWork unitOfWork)
        {
            Database = unitOfWork;
        }

        public IEnumerable<BidDTO> GetBids()
        {
            return Mapper.Map<IEnumerable<Bid>, List<BidDTO>>(Database.Biddings.GetAll());
        }

        public void MakeBid(BidDTO biddingDTO)
        {
            var user = Database.Users.Get(biddingDTO.UserId);
            var lot = Database.Lots.Get(biddingDTO.LotId);

            var bid = new Bid
            {
                User = user,
                Lot = lot,
                Sum = biddingDTO.Sum,
                Date = biddingDTO.Date
            };

            Database.Biddings.Create(bid);

            lot.Biddings.Add(bid);
            lot.CurrentPrice += biddingDTO.Sum;
            Database.Lots.Update(lot);

            user.Biddings.Add(bid);

            Database.Save();
        }
    }
}
