using System.Security.Cryptography.X509Certificates;

namespace InventoryMgmt.Model.ApiUseModel
{
//    Add Item DTO
//itemCode, itemName, brandName, unitOfMeasurement, purchaseRate, salesRate, quantity, expiryDate, storeName,
    public class ItemFormModel
    {
        public string itemName { get; set; }
        public string itemCode { get; set; }
        public string brandName { get; set; }
        public string unitOfMeasurement { get; set; }
        public decimal purchaseRate { get; set; }
        public decimal salesRate { get; set;}
        public decimal quantity { get; set; }
        public DateTime expiryDate { get; set; }


    }
}
