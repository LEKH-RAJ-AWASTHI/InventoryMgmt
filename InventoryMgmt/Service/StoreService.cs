using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;


namespace InventoryMgmt.Service
{
    public class StoreService : IStoreService
    {
        private readonly ApplicationDbContext _context;
        public StoreService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddStore(string storeName)
        {
            if (storeName is null && storeName is "")
            {
                return false;
            }
            StoreModel storeModel = new StoreModel();
            storeModel.storeName = storeName;
            storeModel.isActive = true;
            _context.stores.Add(storeModel);
            _context.SaveChanges();
            return true;
        }
        public bool UpdateStore(string oldStoreName, string newStoreName)
        {
            if (oldStoreName is null && oldStoreName is "")
            {
                return false;
            }
            var storeFromServer = _context.stores.Where(s => s.storeName == oldStoreName).FirstOrDefault();
            if (storeFromServer is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail");
            }
            storeFromServer.storeName = newStoreName;
            _context.SaveChanges();
            return true;
        }
        public bool ChangeStoreActiveStatus(string storeName)
        {
            if (storeName is null && storeName is "")
            {
                return false;
            }
            var storeFromServer = _context.stores.Where(s => s.storeName == storeName).FirstOrDefault();
            if (storeFromServer is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail");
            }
            storeFromServer.isActive = !storeFromServer.isActive;
            _context.SaveChanges();
            return true;
        }
        public List<StoreRegisterModel> ShowAllStores()
        {
            //List<StoreModel> storeList = new List<StoreModel>();
            var storeList= _context.stores.Where(s => s.isActive  == true).ToList<StoreModel>();
            StoreRegisterModel srm = new StoreRegisterModel();
            List<StoreRegisterModel> srmList = new List<StoreRegisterModel>();
            foreach( var store in storeList )
            {
                srm.storeId= store.storeId;
                srm.storeName= store.storeName;
                srmList.Add(srm);
            }
            return srmList;
        }
        public List<dynamic> showStockLevel()
        {
            List<dynamic> list = new List<dynamic>();
            //var stockLevel = from item in _context.items
            //                 join stock in _context.stocks on item.ItemId equals stock.itemId
            //                 join store in _context.stores on stock.storeId equals store.storeId
            //                 select new
            //                 {
            //                     item,
            //                     stock,
            //                     store
            //                 };
            var stockLevel = (from item in 
                                 (from item in _context.items
                                 select new
                                 {
                                     item.ItemId,
                                     item.ItemName,
                                     item.ItemCode,
                                     item.BrandName
                                 })
                             join stock in 
                                 (from stock in _context.stocks
                                  select new
                                  {
                                    stock.itemId,
                                    stock.storeId,
                                    stock.quantity
                                  })
                                  on item.ItemId equals stock.itemId

                             join store in
                                (from store in _context.stores
                                 select new
                                 {
                                     store.storeId, store.storeName,
                                 })
                             
                              on stock.storeId equals store.storeId
                             select new
                             {
                                 store.storeName,
                                 item.ItemName,
                                 item.ItemCode,
                                 item.BrandName,
                                 stock.quantity,
                             }).ToList<dynamic>();
            return stockLevel;
            
        }
        
    }
}
