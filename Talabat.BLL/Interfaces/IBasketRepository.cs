using System.Threading.Tasks;
using Talabat.DAL.Entities;

namespace Talabat.BLL.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetCustomerBasket(string basketId);  
        Task<CustomerBasket> UpdateCustomerBasket(CustomerBasket basket); 
        Task<bool> DeleteCustomerBasket(string basketId);
    }
}
