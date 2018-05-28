using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TermPaper.Models;
using Microsoft.AspNet.Identity;
using BLL.DTO;
using BLL.Interfaces;

namespace TermPaper.Util
{
    public static class CurrentUserUtil
    {
        private static MappingHelper helper = MappingHelper.GetInstance();

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
            var bidModels = helper.Mapper
                .Map<IEnumerable<BiddingDTO>, ICollection<BiddingModel>>(bidDTOs)
                .Where(x => x.UserId == CurrentUserId)
                .ToList();

            return bidModels;
        }
    }
}