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
            if (storeName == null)
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
            if (oldStoreName is null)
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
            if (storeName is null)
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
    }
}
