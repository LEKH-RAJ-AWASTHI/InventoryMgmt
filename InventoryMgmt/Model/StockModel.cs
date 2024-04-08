using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryMgmt.Model
{
    //tbl_stocks(stockId, storeId, itemId, quantity, expiryDate)
    public class StockModel
    {
        [Key]
        public int stockId { get; set; }
        [Required]
        public int storeId { get; set; }
        public StoreModel store { get; set; }
        [Required]
        public int itemId { get; set; }
        public ItemModel item { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal quantity { get; set; }
        [Required]
        public DateTime expiryDate { get; set; }
    }
}
