using InventoryMgmt.Model.ApiUseModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryMgmt.Model
{
    public class ItemModel : ItemModelClass
    {

        [Required]
        public bool IsActive { get; set; }

        public List<StockModel> stocks { get; set; }
    }
}
