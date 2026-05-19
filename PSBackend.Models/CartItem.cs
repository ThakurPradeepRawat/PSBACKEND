namespace PSBackend.Models;

public class CartItem
{
    public int CartItemId { get; set; }
    public int CartId { get; set; }
    public int PrasadId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public DateTime AddedAt { get; set; }
}
