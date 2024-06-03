namespace InventoryMgmt;

public class HubMessageDTO
{
    public string Item { get; set; }
    public string StoreName { get; set; }
    public decimal Quantity { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
}
