using System;
using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Interfaces
{
    public interface IUnitOfWork: IDisposable 
    {
        IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity; 
        Task<int> Complete();
    }
}
