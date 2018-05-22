using AutoMapper;
using BLL.DTO;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
	public static class MapperConfig
	{
		public static void Initialize()
		{
			Mapper.Initialize(config =>
			{
                config.CreateMap<User, UserDTO>();
                config.CreateMap<Lot, LotDTO>();
                config.CreateMap<Category, CategoryDTO>();
            });

		}
	}
}
