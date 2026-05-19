namespace PSBackend.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }
    public int OrderId { get; set; }
    public int PrasadId { get; set; }
    public string PrasadName { get; set; } = string.Empty;
    public string TempleName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
    public int? ReviewId { get; set; }
}

public class CreateOrderItemInputModel
{
    public int OrderId { get; set; }
    public int PrasadId { get; set; }
    public string PrasadName { get; set; }
    public string TempleName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CreateOrderItemOutputModel
{
    public int OrderItemId { get; set; }
}

public class GetOrderItemsInputModel
{
    public int OrderId { get; set; }
}

public class GetOrderItemsOutputModel : OrderItem
{
}
