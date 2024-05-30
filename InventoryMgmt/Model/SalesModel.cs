using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model
{
    public class SalesModel
    {
        [Key]
        public int salesId { get; set; }
        public DateTime dateTime { get; set; }

        public int itemId { get; set; }
        public ItemModel item;

        public decimal Quantity { get; set; }
        public decimal SalesPrice { get; set; }
        public decimal TotalPrice { get; set; }


        public int storeId { get; set; }
        public StoreModel store;


    }
}
