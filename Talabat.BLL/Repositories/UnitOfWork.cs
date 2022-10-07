﻿using System.Collections;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.DAL;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<int> Complete()
        {
           return await _storeContext.SaveChangesAsync();
        }

        public void Dispose() 
        {
            _storeContext.Dispose(); 
        }

        public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories == null)
                _repositories = new Hashtable(); 

            var type = typeof(TEntity).Name;
            if(!_repositories.ContainsKey(type)) 
            {
                var repository = new GenericRepository<TEntity>(_storeContext);                                                           //ask one
                _repositories.Add(type, repository);  
            }      
            return (IGenericRepository<TEntity>) _repositories[type];
        }
    }
}
