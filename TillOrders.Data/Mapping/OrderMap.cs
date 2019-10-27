using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TillOrders.Domain.Model;

namespace TillOrders.Data.Mapping
{
    public partial class OrderMap : CustomEntityTypeConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");
            builder.HasKey(order => order.Id);
            builder.Property(attr => attr.Id).HasColumnName("OrderId");

            base.Configure(builder);
        }
    }
}
