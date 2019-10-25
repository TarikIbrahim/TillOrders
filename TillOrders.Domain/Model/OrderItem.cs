namespace TillOrders.Domain.Model
{
    public partial class OrderItem : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
    }
}
