public class GetItemModelDTO
{
    public int itemId { get; set; }
    public string itemName { get; set; }

    public string itemCode { get; set; }
    public int storeId { get; set; }

    public string storeName { get; set; }

    public decimal stockRemaining { get; set; }
}