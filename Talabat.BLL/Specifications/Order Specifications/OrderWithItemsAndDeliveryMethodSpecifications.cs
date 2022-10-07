using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.BLL.Specifications.Order_Specifications
{
    public class OrderWithItemsAndDeliveryMethodSpecifications:BaseSpecification<Order>
    {
        // This Constructor used to get all Orders for a Specific User 
        public OrderWithItemsAndDeliveryMethodSpecifications(string buyerEmail)
            :base (O => O.BuyerEmail == buyerEmail)
        {
            // specs will build query to get order for user
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);
            AddOrderByDescending(O => O.OrderDate);
        }

        // This Constructor used to get an Order for a Specific User 
        public OrderWithItemsAndDeliveryMethodSpecifications(int orderId , string buyerEmail)
            : base(O => O.BuyerEmail == buyerEmail &&   O.Id == orderId )
        {
            // specs will build query to get order for user
            AddInclude(O => O.Items);
            AddInclude(O => O.DeliveryMethod);
        }
    }
}
