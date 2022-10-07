using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.BLL.Specifications;
using Talabat.BLL.Interfaces;
using Talabat.DAL;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {    
        private readonly StoreContext _context; 
       
        public GenericRepository(StoreContext context)
        {
            _context = context;
        }
        // no return Nav Props 
        public async Task<T> GetAsync(int id)
             => await _context.Set<T>().FindAsync(id);
        public async Task<IReadOnlyList<T>> GetAllAsync()
            => await _context.Set<T>().ToListAsync();
 

        // return Nav Props using Specification Des Patt to create dynamic query 'work Gen'
        public async Task<T> GetEntityWithSpecAsync(ISpecification<T> spec) 

                => await ApplySpecifications(spec).FirstOrDefaultAsync();
        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec)

           => await ApplySpecifications(spec).ToListAsync();

        private IQueryable<T> ApplySpecifications(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery( _context.Set<T>() , spec);
        }

        // Pagination
        public async Task<int> GetCountAsync(ISpecification<T> spec)

            => await ApplySpecifications(spec).CountAsync();

        // Order
        public async Task Add(T entity) 

            => await _context.Set<T>().AddAsync(entity);

        public void Update(T entity)
            =>  _context.Set<T>().Update(entity); 

        public void Delete(T entity)
            => _context.Set<T>().Remove(entity); 


    }
}
