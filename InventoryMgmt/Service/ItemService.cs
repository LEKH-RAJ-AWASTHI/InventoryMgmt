using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;

namespace InventoryMgmt.Service
{
    public class ItemService : IItemService
    {
        ApplicationDbContext db = new ApplicationDbContext();

        public void Add(ItemModel item) { }
        public void Update(ItemModel item) { }
        public void Delete(ItemModel item) { }
        public ItemModel Get(int id)
        {
            return new ItemModel();
        }
        public IEnumerable<ItemModel> GetAll()
        {
            return db.items.ToList<ItemModel>();
        }
    }
}
