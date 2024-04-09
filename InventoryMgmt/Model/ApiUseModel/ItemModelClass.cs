using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model.ApiUseModel
{
    public class ItemModelClass
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string ItemCode { get; set; }

        [Required]
        public string ItemName { get; set; }

        [Required]
        public string BrandName { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchaseRate { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalesRate { get; set; }



    }
}
