using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace InventoryMgmt.Model
{
    //tbl_stores(storeId, storeName, isActive)
    public class StoreModel
    {
        [Key]
        public int storeId {  get; set; }
        [Required]
        public string storeName { get; set; }
        [Required]
        public string isActive
        {
            get; set;
        }

        public List<StockModel> stocks { get; set; }
    }
}
