using Mauve.Core.Common;

namespace Mauve.Core.Models;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal SubTotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }
    public List<OrderItem> Items { get; set; } = new();
    public Address ShippingAddress { get; set; }
    public PaymentInfo PaymentInfo { get; set; }
    public string TrackingNumber { get; set; }
    public string ShippingCarrier { get; set; }
}

public enum OrderStatus
{
    Pending,
    PaymentConfirmed,
    Processing,
    Shipped,
    Delivered,
    Cancelled,
    RefundRequested,
    Refunded
}

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public Guid VariantId { get; set; }
    public int Quantity { get; set; }
    public decimal PriceAtTime { get; set; }
    public Order Order { get; set; }
    public Product Product { get; set; }
    public ProductVariant Variant { get; set; }
}

public class PaymentInfo
{
    public string PaymentIntentId { get; set; }
    public string PaymentMethod { get; set; }
    public PaymentStatus Status { get; set; }
    public DateTime? PaidAt { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Succeeded,
    Failed,
    Refunded
}