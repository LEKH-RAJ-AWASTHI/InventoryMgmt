using InventoryMgmt.Model.ApiUseModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace InventoryMgmt.Model
{
    //tbl_stores(storeId, storeName, isActive)
    public class StoreModel : StoreRegisterModel
    {
        public bool isActive
        {
            get; set;
        }

        public List<StockModel> stocks { get; set; }
        public List<SalesModel> sales { get; set; }

    }
}
