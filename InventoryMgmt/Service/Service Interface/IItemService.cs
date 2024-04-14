using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;

namespace InventoryMgmt.Service
{
    public interface IItemService
    {
        bool AddItem(AddItemFormModel item);
        bool ChangeItemActiveStatus(int itemId);
        ItemModelClass Get(int itemId);
        IEnumerable<ItemModel> GetAll();
        bool Update(int itemId, ItemFormModel item);
    }
}