using InventoryMgmt.Model;

namespace InventoryMgmt.Service
{
    public interface IItemService
    {
        void Add(ItemModel item);
        void Delete(ItemModel item);
        ItemModel Get(int id);
        IEnumerable<ItemModel> GetAll();
        void Update(ItemModel item);
    }
}