using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TermPaper.Models;
using BLL.DTO;
using TermPaper.Util;

namespace TermPaper.Controllers
{
    public class BiddingController : Controller
    {
        private IUserService userService;
        private ILotService lotService;
        private IBidService bidService;
        private MappingHelper helper;
        private CurrentUserModel CurrentUser
        {
            get
            {
                var id = HttpContext.User.Identity.GetUserId();

                var bidDTOs = bidService.GetBids();
                var bidModels = helper.Mapper
                    .Map<IEnumerable<BiddingDTO>, ICollection<BiddingModel>>(bidDTOs)
                    .Where(x => x.UserId == HttpContext.User.Identity.GetUserId())
                    .ToList();

                var userModel = new CurrentUserModel
                {
                    Id = id,
                    Biddings = bidModels
                };

                return userModel;
            }
        }

        public BiddingController(IUserService userService, ILotService lotService, IBidService bidService)
        {
            this.userService = userService;
            this.lotService = lotService;
            this.bidService = bidService;
            helper = MappingHelper.GetInstance();
        }

        public ActionResult Index()
        {
            var bids = CurrentUser.Biddings;

            return View(bids);
        }

        public ActionResult Bid(int id)
        {
            var bid = new BiddingDTO
            {
                Sum = lotService.GetLotById(id).BidRate,
                UserId = CurrentUser.Id,
                LotId = id,
                Date = DateTime.Now
            };

            bidService.MakeBid(bid);

            return View("AcceptedBid");
        }
    }
}