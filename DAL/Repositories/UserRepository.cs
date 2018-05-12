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
    public class UserRepository : IUserRepository
    {
        public AuctionContext Database { get; set; }

        public UserRepository(AuctionContext db)
        {
            Database = db;
        }

        public IEnumerable<User> GetAll()
        {
            return Database.ApplicationUsers;
        }

        public User Get(string id)
        {
            return Database.ApplicationUsers.Find(id);
        }

        public void Create(User item)
        {
            Database.ApplicationUsers.Add(item);
        }

        public void Update(User item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            User user = Database.ApplicationUsers.Find(id);
            if (user != null)
                Database.ApplicationUsers.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return Database.ApplicationUsers.Where(predicate).ToList();
        }
    }
}
