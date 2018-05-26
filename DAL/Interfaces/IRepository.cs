using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<Entity> where Entity : class
    {
        IEnumerable<Entity> GetAll();
        Entity Get(string id);  //for getting users
        Entity Get(int id);  //for getting other DB entries
        void Create(Entity item);
        void Update(Entity item);
        void Delete(Entity item);
        IEnumerable<Entity> Find(Func<Entity, bool> predicate);
    }
}
