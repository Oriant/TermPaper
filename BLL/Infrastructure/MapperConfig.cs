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
		public static void Init()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<User, UserDTO>();
			});

		}

		public static void LotMapper()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Lot, LotDTO>();
			});
		}

		public static void CategoryMapper()
		{
			Mapper.Initialize(cfg =>
			{
				cfg.CreateMap<Category, CategoryDTO>();
			});
		}

	}
}
