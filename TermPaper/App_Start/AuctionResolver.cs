using BLL.Infrastructure;
using Ninject;
using Ninject.Modules;
using Ninject.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TermPaper.Util;

namespace TermPaper.App_Start
{
    public static class AuctionResolver
    {
        public static void Configure()
        {
            MapperConfig.Initialize();
            NinjectModule serviceMoudle = new ServiceModule();
            NinjectModule bindingModule = new BindingModule("DefaultConnection");

            var kernel = new StandardKernel(serviceMoudle, bindingModule);
            kernel.Unbind<ModelValidatorProvider>();
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}