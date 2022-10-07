using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.BLL.Specifications;

namespace Talabat.BLL.Interfaces
{
    public interface IGenericRepository<T> 
        
    {
        // two signatures for Meths to Get Entity as Order ,Product 'no return Navigational Props as ProdType,PBrand.null.'
        Task<T> GetAsync(int id); //  get Entity with id
        Task<IReadOnlyList<T>> GetAllAsync(); // //  GetAll Entities

        // two signatures for Meths to Get Entity but return Nav Props work Eagar loading using include 
        Task<T> GetEntityWithSpecAsync(ISpecification<T> spec); // get with id 
        Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecification<T> spec); //  GetAll Entities

        // Pagination 
        Task<int> GetCountAsync(ISpecification<T> spec);

        // Order 
        Task Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
