using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.DAL.Data.Config
{
    public class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.OwnsOne(item => item.ItemOrdered, Product => Product.WithOwner());
            builder.Property(item => item.Price) 
                .HasColumnType("decimal(18, 2)"); 
        }
    }
}
