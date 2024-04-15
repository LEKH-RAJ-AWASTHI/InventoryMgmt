using System.Security.Cryptography.X509Certificates;

namespace InventoryMgmt.Model.ApiUseModel
{
//    Add Item DTO
//itemCode, itemName, brandName, unitOfMeasurement, purchaseRate, salesRate, quantity, expiryDate, storeName,
    public class AddItemFormModel : ItemFormModel
    {
        public string   storeName { get; set; }
    }
}
