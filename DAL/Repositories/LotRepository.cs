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
	public class LotRepository : IRepository<Lot>
	{
		private AuctionContext db;

		public LotRepository(AuctionContext context)
		{
			this.db = context;
		}
		public void Create(Lot item)
		{
			db.Lots.Add(item);
		}

		public void Delete(int id)
		{
			Lot item = db.Lots.Find(id); 
			if(item != null)
			{
				db.Lots.Remove(item);
			}
		}

		public IEnumerable<Lot> Find(Func<Lot, bool> predicate)
		{
			return db.Lots.Where(predicate).ToList();
		}

		public Lot Get(int id)
		{
			return db.Lots.Find(id);
		}

		public IEnumerable<Lot> GetAll()
		{
			return db.Lots;
		}

		public void Update(Lot item)
		{
			db.Entry(item).State = System.Data.Entity.EntityState.Modified;
		}
	}
}
