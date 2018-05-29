using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using DAL.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
	public static class MapperConfig
	{
		public static void Configure(IMapperConfigurationExpression conf)
		{
            conf.CreateMap<Lot, LotDTO>();
            conf.CreateMap<Category, CategoryDTO>();
            conf.CreateMap<Bid, BidDTO>();
            conf.CreateMap<User, UserDTO>();
            conf.CreateMap<ApplicationUser, UserDTO>();
		}
	}
}
