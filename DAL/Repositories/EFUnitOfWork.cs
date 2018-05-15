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
		private UserRepository userRepository;
		private CategoryRepository categoryRepository;
		private LotRepository lotRepository;
		private bool disposed = false;

		public EFUnitOfWork(string connectionString)
		{
			db = new AuctionContext(connectionString);
		}
		public IRepository<Lot> Lots
		{
			get
			{
				if (lotRepository == null)
					lotRepository = new LotRepository(db);
				return (lotRepository);
			}
		}
		public IRepository<Category> Categories
		{
			get
			{
				if (categoryRepository == null)
					categoryRepository = new CategoryRepository(db);
				return (categoryRepository);
			}
		}
		public IUserRepository Users
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);

                return userRepository;
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
