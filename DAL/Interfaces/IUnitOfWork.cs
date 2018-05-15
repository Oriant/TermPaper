using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
		IRepository<Lot> Lots { get; }
		IRepository<Category> Categories { get; }
        void Save();
    }
}
