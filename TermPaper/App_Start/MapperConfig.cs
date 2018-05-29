using AutoMapper;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TermPaper.Models;

namespace TermPaper.App_Start
{
    public class MapperConfig
    {
        private static void Configure(IMapperConfigurationExpression conf)
        {
            conf.CreateMap<LotDTO, LotModel>();
            conf.CreateMap<BidDTO, BiddingModel>();
            conf.CreateMap<CategoryDTO, CategoryModel>();
        }

        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                Configure(cfg);
                BLL.Infrastructure.MapperConfig.Configure(cfg);
            });
        }
    }
}