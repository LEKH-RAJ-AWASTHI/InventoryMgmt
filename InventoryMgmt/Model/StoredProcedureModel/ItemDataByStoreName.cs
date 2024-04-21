namespace InventoryMgmt.Model.StoredProcedureModel
{
    public class ItemDataByStoreName
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string StoreName { get; set; }
        public string ItemCode { get; set; }
        public string BrandName { get; set; }
        public string UnitOfMeasurement { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal SalesRate { get; set; }
        public decimal Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
