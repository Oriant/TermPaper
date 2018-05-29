using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TermPaper.Models;
using Microsoft.AspNet.Identity;
using BLL.DTO;
using BLL.Interfaces;
using AutoMapper;

namespace TermPaper.Util
{
    public static class CurrentUserUtil
    {
        public static string CurrentUserId
        {
            get
            {
                return HttpContext.Current.User.Identity.GetUserId();
            }
        }

        public static ICollection<BiddingModel> CurrentUserBids(IBidService bidService)
        {
            var id = CurrentUserId;

            var bidDTOs = bidService.GetBids();
            var bidModels = Mapper.Map<IEnumerable<BidDTO>, ICollection<BiddingModel>>(bidDTOs)
                .Where(x => x.UserId == CurrentUserId)
                .ToList();

            return bidModels;
        }
    }
}