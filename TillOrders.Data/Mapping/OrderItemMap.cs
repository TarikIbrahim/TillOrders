using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TillOrders.Domain.Model;

namespace TillOrders.Data.Mapping
{
    public partial class OrderItemMap : CustomEntityTypeConfiguration<OrderItem>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItem");
            builder.HasKey(orderItem => orderItem.Id);
            builder.Property(attr => attr.Id).HasColumnName("OrderItemId");

            builder.HasOne(orderItem => orderItem.Order)
                .WithMany(order => order.OrderItems)
                .HasForeignKey(orderItem => orderItem.OrderId)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
