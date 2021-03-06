﻿using BLL.Interfaces;
using BLL.Services;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TermPaper.Util
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
			Bind<IUserService>().To<UserService>();
			Bind<ICategoryService>().To<CategoryService>();
			Bind<ILotService>().To<LotService>();
            Bind<IManagerService>().To<ManagerService>();
            Bind<IBidService>().To<BidService>();
		}
    }
}