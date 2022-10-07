using System;
using System.Collections.Generic;


namespace Talabat.DAL.Entities.Order_Aggregate
{
    public class Order:BaseEntity
    {
        public Order()
        {
        }
        public Order(string buyerEmail, Address shipToAddress, DeliveryMethod deliveryMethod,
            List<OrderItem> items, decimal subtotal  )
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            Subtotal = subtotal;
        }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate  { get; set; } = DateTimeOffset.Now; 
        public Address ShipToAddress { get; set; } 
        public DeliveryMethod DeliveryMethod  { get; set; }
        public OrderStatus Status { get; set; } = OrderStatus.Pending; 
        public List<OrderItem> Items { get; set; } // Nav Prop // work Eager L
        public string PaymentIntenId { get; set; } 
        public decimal Subtotal { get; set; } 

        public decimal GetTotal() // no prop as no need col to save it in Db as calculated in runtime 
            => Subtotal + DeliveryMethod.Cost; 

    }
}
