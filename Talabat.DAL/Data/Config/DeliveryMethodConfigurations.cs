using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Talabat.DAL.Entities.Order_Aggregate;

namespace Talabat.DAL.Data.Config
{
    public class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(DM => DM.Cost) 
                 .HasColumnType("decimal(18, 2)");
        }
    }
}
