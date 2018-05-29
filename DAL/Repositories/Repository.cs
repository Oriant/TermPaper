using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class Repository<Entity> : IRepository<Entity> where Entity : class
    {
        private readonly AuctionContext context;
        private readonly DbSet<Entity> dbSet;

        public Repository(AuctionContext context)
        {
            this.context = context;
            dbSet = context.Set<Entity>();
        }


        public IEnumerable<Entity> GetAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public Entity Get(int id)
        {
            return dbSet.Find(id);
        }

        public Entity Get(string id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<Entity> Find(Func<Entity, bool> predicate)
        {
            return dbSet.AsNoTracking().Where(predicate).ToList();
        }

        public void Create(Entity item)
        {
            dbSet.Add(item);
            context.SaveChanges();
        }

        public void Update(Entity item)
        {
            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }

        public void Delete(Entity item)
        {
            dbSet.Remove(item);
            context.SaveChanges();
        }
    }
}
