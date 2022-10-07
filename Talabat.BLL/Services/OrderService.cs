using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Talabat.BLL.Interfaces;
using Talabat.BLL.Specifications.Order_Specifications;
using Talabat.DAL.Entities;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IBasketRepository basketRepository , IUnitOfWork unitOfWork )
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Order> CreateOrderAsync(string buyerEmail, string BasketId, int deliveryMethodId, Address shipToAddress)
        {
            // 1. Get Basket From BasRep so inj IBasRep
            var basket = await _basketRepository.GetCustomerBasket(BasketId);

            // 2. get selected items 'Prods' at basket from Database by its Id
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await _unitOfWork.Repository<Product>().GetAsync(item.Id); 

                var productItemOrdered = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);
                var orderItem = new OrderItem(productItemOrdered, product.Price, item.Quantity);
                orderItems.Add(orderItem);
            }
            // 3. get delivery method  
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetAsync(deliveryMethodId);
            // 4. calculate subtotal 
            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 5. create order then add it in Db 
            var order = new Order(buyerEmail, shipToAddress, deliveryMethod, orderItems, subtotal);
            await _unitOfWork.Repository<Order>().Add(order); 

            // 6. save to Db 
            int result = await _unitOfWork.Complete();
            if(result <= 0)
                return null; 
            return order; 
        }
        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(buyerEmail);
            var orders = await _unitOfWork.Repository<Order>().GetAllWithSpecAsync(spec);
            return orders;
        }
        public  async Task<Order> GetOrderByIdForUserAsync(int orderId, string buyerEmail)
        {
            var spec = new OrderWithItemsAndDeliveryMethodSpecifications(orderId, buyerEmail);
            var orders =  await _unitOfWork.Repository<Order>().GetEntityWithSpecAsync(spec);
            return orders;
        }
        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().GetAllAsync();
        }

    }
}
