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
            return Database.Users;
        }

        public User Get(string id)
        {
            return Database.Users.Find(id);
        }

        public void Create(User item)
        {
            Database.Users.Add(item);
        }

        public void Update(User item)
        {
            Database.Entry(item).State = EntityState.Modified;
        }

        public void Delete(string id)
        {
            User user = Database.Users.Find(id);
            if (user != null)
                Database.Users.Remove(user);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return Database.Users.Where(predicate).ToList();
        }
    }
}
