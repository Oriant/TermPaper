using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
	public class EFUnitOfWork : IUnitOfWork
	{
		private AuctionContext db;
		private Repository<User> userRepository;
		private Repository<Category> categoryRepository;
		private Repository<Lot> lotRepository;
        private Repository<Bid> biddingRepository;
		private bool disposed = false;

        public EFUnitOfWork(string connectionString) => db = new AuctionContext(connectionString);


        public IRepository<Lot> Lots
		{
			get
			{
				if (lotRepository == null)
					lotRepository = new Repository<Lot>(db);
				return (lotRepository);
			}
		}

		public IRepository<Category> Categories
		{
			get
			{
				if (categoryRepository == null)
					categoryRepository = new Repository<Category>(db);
				return (categoryRepository);
			}
		}

		public IRepository<User> Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new Repository<User>(db);

                return userRepository;
            }
        }

        public IRepository<Bid> Biddings
        {
            get
            {
                if (biddingRepository == null)
                    biddingRepository = new Repository<Bid>(db);

                return biddingRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
