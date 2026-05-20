namespace PSBackend.Models;

public class Order
{
    public int OrderId { get; set; }
    public string OrderNumber { get; set; } = string.Empty;
    public int UserId { get; set; }
    public int AddressId { get; set; }
    public int? CouponId { get; set; }
    public string Status { get; set; } = "Pending";
    public decimal SubTotal { get; set; }
    public decimal DeliveryCharges { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string DeliveryType { get; set; } = "Standard";
    public string? SpecialInstructions { get; set; }
    public DateTime PlacedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

public class CreateOrderInputModel
{
    public string? OrderNumber { get; set; }
    public int UserId { get; set; }
    public int AddressId { get; set; }
    public int? CouponId { get; set; }
    public decimal SubTotal { get; set; }
    public decimal DeliveryCharges { get; set; }
    public decimal DiscountAmount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public string? DeliveryType { get; set; }
    public string? SpecialInstructions { get; set; }
}

public class CreateOrderOutputModel
{
    public int OrderId { get; set; }
}

public class GetOrderByIdInputModel
{
    public int OrderId { get; set; }
}

public class GetOrderByIdOutputModel : Order
{
}
