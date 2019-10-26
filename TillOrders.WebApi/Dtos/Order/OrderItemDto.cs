namespace TillOrders.WebApi.Dtos.Order
{
    public class OrderItemDto
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public decimal Price { get; set; }

        public virtual OrderDto Order { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
    }
}
