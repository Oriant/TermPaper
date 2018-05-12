using DAL.EF;
using DAL.Entities;
using DAL.Identity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Identity.Repositories
{
    public class ClientManager : IClientManager
    {
        public AuctionContext Database { get; set; }

        public ClientManager(AuctionContext db)
        {
            Database = db;
        }

        public void Create(User item)
        {
            Database.Users.Add(item);
            Database.SaveChanges();
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
