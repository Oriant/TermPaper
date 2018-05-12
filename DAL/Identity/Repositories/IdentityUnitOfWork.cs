using DAL.EF;
using DAL.Identity.Entities;
using DAL.Identity.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWorkIdentity
    {
        private AuctionContext db;
        private bool disposed = false;

        public ApplicationUserManager UserManager { get; protected set; }
        public ApplicationRoleManager RoleManager { get; protected set; }
        public IClientManager ClientManager { get; protected set; }


        public IdentityUnitOfWork(string connectionString)
        {
            db = new AuctionContext(connectionString);
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            RoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            ClientManager = new ClientManager(db);
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    UserManager.Dispose();
                    RoleManager.Dispose();
                    ClientManager.Dispose();
                }
                disposed = true;
            }
        }
    }
}
