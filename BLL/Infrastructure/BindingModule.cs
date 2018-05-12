using BLL.Interfaces;
using BLL.Services;
using DAL.Identity.Interfaces;
using DAL.Identity.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class BindingModule : NinjectModule
    {
        private string connectionString;

        public BindingModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWorkIdentity>().To<IdentityUnitOfWork>().WithConstructorArgument(connectionString);
            Bind<IUserService>().To<UserService>();
        }
    }
}
