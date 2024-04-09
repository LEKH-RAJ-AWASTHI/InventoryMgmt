using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model.ApiUseModel
{
    public class StoreRegisterModel
    {
        public string storeName { get; set; }
        public string isActive
        {
            get; set;
        }
    }
}
