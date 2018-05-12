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
        Entity Get(int id);
        void Create(Entity item);
        void Update(Entity item);
        void Delete(int id);
        IEnumerable<Entity> Find(Func<Entity, bool> predicate);
    }
}
