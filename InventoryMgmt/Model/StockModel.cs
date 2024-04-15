using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryMgmt.Model
{
    //tbl_stocks(stockId, storeId, itemId, quantity, expiryDate)
    public class StockModel
    {
        [Key]
        public int stockId { get; set; }
        public int storeId { get; set; }
        public StoreModel store { get; set; }
        public int itemId { get; set; }
        public ItemModel item { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal quantity { get; set; }
        public DateTime expiryDate { get; set; }
    }
}
