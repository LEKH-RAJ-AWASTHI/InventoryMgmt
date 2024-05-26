using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;

namespace InventoryMgmt.Service
{
    public interface IItemService
    {
        bool AddItem(AddItemFormModel item);
        bool ChangeItemActiveStatus(int itemId);
        ItemModelClass Get(int itemId);
        IEnumerable<GetItemModelDTO> GetAll();
        bool Update(int itemId, ItemFormModel item);
        bool InsertBulkItems(int storeId, List<ItemFormModel> items);
    }
}