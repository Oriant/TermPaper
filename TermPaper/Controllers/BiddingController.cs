﻿using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TermPaper.Models;
using BLL.DTO;
using TermPaper.Util;
using AutoMapper;

namespace TermPaper.Controllers
{
    public class BiddingController : Controller
    {
        private readonly IUserService userService;
        private readonly ILotService lotService;
        private readonly IBidService bidService;


        public BiddingController(IUserService userService, ILotService lotService, IBidService bidService)
        {
            this.userService = userService;
            this.lotService = lotService;
            this.bidService = bidService;
        }

        public ActionResult Index()
        {
            var id = HttpContext.User.Identity.GetUserId();

            var bidDTOs = bidService.GetBids();
            var bidModels = Mapper.Map<IEnumerable<BidDTO>, ICollection<BiddingModel>>(bidDTOs)
                .Where(x => x.UserId == id)
                .ToList();

            return View(bidModels);
        }

        public ActionResult Bid(int id)
        {
            var bid = new BidDTO
            {
                Sum = lotService.GetLotById(id).BidRate,
                UserId = HttpContext.User.Identity.GetUserId(),
                LotId = id,
                Date = DateTime.Now
            };

            bidService.MakeBid(bid);

            return View("AcceptedBid");
        }
    }
}