using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model.ApiUseModel
{
    public class ItemModelClass
    {
        [Key]
        public int ItemId { get; set; }
        public string ItemCode { get; set; }
        public int ItemNo { get; set; }
        public string ItemName { get; set; }
        public string BrandName { get; set; }
        public string UnitOfMeasurement { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PurchaseRate { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal SalesRate { get; set; }

        public string GetItemCode( int itemNo)
        {
            return $"ITM-{itemNo + 1}";
        }



    }
}
