using AutoMapper;
using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TermPaper.Models;

namespace TermPaper.Util
{
    class MappingHelper
    {
        private static MappingHelper instance;

        public IMapper Mapper { get; private set; }

        private MappingHelper()
        {
            Mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<LotDTO, LotModel>();
                cfg.CreateMap<CategoryDTO, CategoryModel>();
                cfg.CreateMap<BiddingDTO, BiddingModel>();
            }).CreateMapper();
        }

        public static MappingHelper GetInstance()
        {
            if (instance == null)
                instance = new MappingHelper();

            return instance;
        }
    }
}