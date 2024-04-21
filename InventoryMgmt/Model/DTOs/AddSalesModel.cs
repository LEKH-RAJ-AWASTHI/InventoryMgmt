namespace InventoryMgmt.Model.DTOs
{
    public class AddSalesModel
    {
        public int StoreId {  get; set; }
        public int ItemId { get; set; } 
        public decimal Quantity { get; set; }
    }
}
