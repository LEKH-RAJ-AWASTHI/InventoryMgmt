using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model.ApiUseModel
{
    public class StoreRegisterModel
    {
        [Key]
        public int storeId { get; set; }
        [Required]
        public string storeName { get; set; }

    }
}
