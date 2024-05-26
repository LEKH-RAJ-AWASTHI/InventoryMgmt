using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;

namespace InventoryMgmt.Service
{
    public class ItemService : IItemService
    {
        ItemService service;
        private readonly ApplicationDbContext _context;
        ItemModel itemModel = new ItemModel();
        StockModel stockModel = new StockModel();
        private IMemoryCache _memoryCache;
        private readonly string cachekey = "itemCacheKey";

        public ItemService(
            ApplicationDbContext context,
            IMemoryCache memoryCache

            )
        {
            _memoryCache = memoryCache;
            _context = context;
        }

        public bool AddItem(AddItemFormModel item)
        {
            Log.Information("Adding Single Item to Database");
            string itemName = item.itemName;
            string brandName = item.brandName;
            string unitOfMeasurement = item.unitOfMeasurement;
            decimal purchaseRate = item.purchaseRate;
            decimal salesRate = item.salesRate;
            decimal quantity = item.quantity;
            DateTime expiryDate = item.expiryDate;
            string storeName = item.storeName;


            StoreModel serverStoreModel = _context.stores.Where(s => s.storeName == storeName && s.isActive == true).FirstOrDefault();
            if (serverStoreModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. Store not added or deleted. You first have to add new Store");
            }

            //var ItemNo = _context.items.Select(i => i.ItemNo).DefaultIfEmpty(0).Max();
            ////Mapping ItemModel 
            //itemModel.ItemCode = itemModel.GetItemCode(ItemNo);

            //checking for duplicate item model
            ItemModel serverItemModel = _context.items.Where(i => i.ItemCode == itemModel.ItemCode).FirstOrDefault();
            //if (serverItemModel is not null)
            //{
            //    //
            //    //throw new InvalidDataException($"Item code already exist. Please use another item code");
            //    itemModel.ItemCode = itemModel.GetItemCode(ItemNo + 1);

            //}
            //itemModel.ItemNo = ++ItemNo;
            itemModel.ItemName = itemName;
            itemModel.BrandName = brandName;
            itemModel.PurchaseRate = purchaseRate;
            itemModel.SalesRate = salesRate;
            itemModel.UnitOfMeasurement = unitOfMeasurement;
            itemModel.IsActive = true;

            GenerateItemCodeAndRetry(itemModel);
            serverItemModel = _context.items.Where(i => i.ItemCode == itemModel.ItemCode && i.IsActive == true).FirstOrDefault();


            //Mapping StockModel
            stockModel.storeId = serverStoreModel.storeId;
            stockModel.itemId = serverItemModel.ItemId;
            stockModel.quantity = quantity;
            stockModel.expiryDate = expiryDate;
            _context.stocks.Add(stockModel);
            _context.SaveChanges();
            return true;
        }
        public bool Update(int itemId, ItemFormModel item)
        {
            Log.Information("Updating Item ");
            string itemName = item.itemName;
            string brandName = item.brandName;
            string unitOfMeasurement = item.unitOfMeasurement;
            decimal purchaseRate = item.purchaseRate;
            decimal salesRate = item.salesRate;
            decimal quantity = item.quantity;
            DateTime expiryDate = item.expiryDate;
            if (itemId is 0)
            {
                throw new InvalidOperationException($"ItemId cannot be zero");

            }
            //all of below commented validations are replaced by Fluent valdation   

            //if (itemName is "" && brandName is "" && unitOfMeasurement is "")
            //{
            //    throw new InvalidOperationException($"All fields are required");
            //}
            //if (purchaseRate is 0 && salesRate is 0 && quantity is 0)
            //{
            //    throw new InvalidOperationException($"The fields (Purchase Rate, Sales Rate, Quantity) cannot be zero");
            //}
            ItemModel serverItemModel = _context.items.Where(i => i.ItemId == itemId).FirstOrDefault();
            if (serverItemModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. ");
            }
            //mapping ItemModel
            serverItemModel.ItemName = itemName;
            serverItemModel.BrandName = brandName;
            serverItemModel.UnitOfMeasurement = unitOfMeasurement;
            serverItemModel.PurchaseRate = purchaseRate;
            serverItemModel.SalesRate = salesRate;


            //Mapping StockModel
            StockModel serverStockModel = _context.stocks.Where(s => s.itemId == serverItemModel.ItemId).FirstOrDefault();
            if (serverStockModel is null)
            {
                return false;
            }
            serverStockModel.quantity = quantity;
            serverStockModel.expiryDate = expiryDate;
            _context.SaveChanges();

            return true;
        }
        public bool ChangeItemActiveStatus(int itemId)
        {
            Log.Information($"Changing Active status of the Item. ItemId {itemId}");
            if (itemId is 0)
            {
                Log.Error("ItemId Cannot be zero");
                throw new InvalidOperationException($"ItemId cannot be zero");
            }

            ItemModel serverItemModel = _context.items.Where(i => i.ItemId == itemId).FirstOrDefault();
            if (serverItemModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. ");
            }
            //if (serverItemModel.IsActive == false) 
            //{
            //    serverItemModel.IsActive = true;
            //}
            //else
            //{
            //    serverItemModel.IsActive= false;
            //}
            //this is shortcut method of above 8 lines
            serverItemModel.IsActive = !serverItemModel.IsActive;

            //StockModel serverStockModel = _context.stocks.Where(s => s.itemId == serverItemModel.ItemId).FirstOrDefault();
            //if (serverStockModel is null)
            //{
            //    return false;
            //}
            //serverStockModel.quantity = 0;
            _context.SaveChanges();
            return true;
        }
        public ItemModelClass Get(int itemId)
        {
            Log.Information($"Getting information of Item having itemId {itemId}");
            if (itemId is 0)
            {
                Log.Error("ItemId Cannot be zero");
                throw new InvalidOperationException($"ItemId cannot be zero");
            }

            ItemModel serverItemModel = _context.items.Where(i => i.ItemId == itemId).FirstOrDefault();

            

            if (serverItemModel is null)
            {

                throw new InvalidOperationException($"Cannot find matching detail. ");
            }

            return serverItemModel;
        }
        public IEnumerable<GetItemModelDTO> GetAll()
        {
            List<GetItemModelDTO> GetItemList = new List<GetItemModelDTO>();
            Log.Information("Getting Information of all Items");

            if (_memoryCache.TryGetValue(cachekey, out List<ItemModel>items))
            {
                Log.Information("Items found in the cache");
            }
            else
            {
                Log.Information("Items not found in cache");
                items = _context.items.Where(i => i.IsActive == true).ToList<ItemModel>();
                var getItemDTO= from stock in _context.stocks join
                                store in _context.stores on stock.storeId equals store.storeId join 
                                item in _context.items on stock.itemId equals item.ItemId
                                select new{
                                    itemId= item.ItemId,
                                    itemName= item.ItemName,
                                    itemCode = item.ItemCode,
                                    storeId = store.storeId,
                                    storeName = store.storeName,
                                    stockRemaining= stock.quantity
                                };

                foreach(var item in getItemDTO)
                {
                    GetItemModelDTO getItem = new GetItemModelDTO
                    {
                        itemId = item.itemId,
                        itemName = item.itemName,
                        itemCode= item.itemCode,
                        storeId= item.storeId,
                        storeName= item.storeName,
                        stockRemaining = item.stockRemaining
                    };

                    GetItemList.Add(getItem);
                }


                var cacheSettings = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(1))
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                    .SetPriority(CacheItemPriority.Normal);
                _memoryCache.Set(cachekey, items, cacheSettings);
            }
            return GetItemList;
        }
        //how to make the function take only one request at one time
        public bool InsertBulkItems(int storeId, List<ItemFormModel> items)
        {
            //using (var scope = new TransactionScope())
            //{

            //}

            List<ItemModel> itemGroup = new List<ItemModel>();
            List<StockModel> stocksGroup = new List<StockModel>();
            using (var context = _context.Database.BeginTransaction())
            {
                try
                {

                    var ItemNo = _context.items.Select(i => i.ItemNo).DefaultIfEmpty(0).Max();

                    foreach (var item in items)
                    {
                        StoreModel serverStoreModel = _context.stores.Where(s => s.storeId == storeId && s.isActive == true).FirstOrDefault();
                        if (serverStoreModel is null)
                        {
                            Log.Error($"Cannot Find Matching detail of StoreId: {storeId}");
                            throw new InvalidOperationException($"Cannot find matching detail. Store not added or deleted. You first have to add new Store");
                        }
                        ItemModel itm = new ItemModel();

                        //getting max Item number in the database
                        //Mapping ItemModel 
                        itm.ItemCode = itm.GetItemCode(ItemNo);

                        //checking for duplicate item model
                        ItemModel serverItemModel = _context.items.Where(i => i.ItemCode == itemModel.ItemCode).FirstOrDefault();
                        if (serverItemModel is not null)
                        {
                            //throw new InvalidDataException($"Item code already exist. Please use another item code");
                            itm.ItemCode = itm.GetItemCode(ItemNo);

                        }
                        itm.ItemNo = ++ItemNo;
                        itm.ItemName = item.itemName;
                        itm.BrandName = item.brandName;
                        itm.UnitOfMeasurement = item.unitOfMeasurement;
                        itm.PurchaseRate = item.purchaseRate;
                        itm.SalesRate = item.salesRate;
                        itm.IsActive = true;
                        itemGroup.Add(itm);
                    }
                    GenerateItemCodeForBulkItem(itemGroup);
                    for (int i = 0; i < itemGroup.Count(); i++)
                    {

                        //StoreModel serverStoreModel = _context.stores.Where(s => s.storeName == item.storeName && s.isActive == true).FirstOrDefault();


                        StockModel stck = new StockModel();
                        stck.storeId = storeId;

                        stck.itemId = itemGroup[i].ItemId;
                        var itmGrp = itemGroup[i];
                        var data = items.FirstOrDefault(i => i.itemName == itmGrp.ItemName && i.brandName == itmGrp.BrandName);
                        /*
                        1. Create ItemCode internally, Do not ask client to give ItemCode
                        2. Create logic that created unique ItemCodes for Every Items
                        3. Duplicate ItemCodes are not allowed in the database
                        4. Add a new column as ItemNo in the Database that will increase for every new Item
                        5. Pattern for the ItemCode should be ITM-{ItemNo} --> ItemNo is incremented for every new Item
                         */
                        if (data is not null)
                        {
                            stck.quantity = data.quantity;
                            stck.expiryDate = data.expiryDate;
                        }

                        stocksGroup.Add(stck);
                    }
                    _context.stocks.AddRange(stocksGroup);


                    _context.SaveChanges();

                    context.Commit();

                }
                catch (Exception ex)
                {
                    Log.Error($"Adding ItemList to database Failed : {ex.Message}");
                    context.Rollback();
                    throw new InvalidOperationException(ex.Message);
                    //return false;
                }
            }
            return true;
        }
        private void GenerateItemCodeAndRetry(ItemModel itm)
        {
            Log.Information("Generating Item code for a Item");
            //int attempt = 0;
            try
            {
                int itemNo = _context.items.Max(i => i.ItemNo);
                itm.ItemCode = itm.GetItemCode(itemNo);
                _context.Add(itm);
                _context.SaveChanges();

            }
            catch (SqlException ex)
            {
                if (ex.Number is 2601)
                {
                    Log.Error($"Error while generating new Item Code : {ex.Message}");

                    GenerateItemCodeAndRetry(itm);
                }
            }
        }
        private void GenerateItemCodeForBulkItem(List<ItemModel> itmList)
        {
            try
            {
                Log.Information("Generating Item Code For Bulk Item");
                for (int i = 0; i < itmList.Count; i++)
                {
                    int itemNo = _context.items.Max(i => i.ItemNo);
                    itmList[i].ItemCode = itmList[i].GetItemCode(itemNo + i);
                }
                _context.items.AddRange(itmList);
                _context.SaveChanges();
            }
            catch (SqlException ex)
            {
                /*
                 Msg 2601, Level 14, State 1, Line 1
Cannot insert duplicate key row in object 'dbo.tbl_item' with unique index 'IX_tbl_item_ItemCode'. The duplicate key value is (MD001).
                 To extract MD001 
                length = 5
                start index = M and end index =after 1 
                 */

                if (ex.Number is 2601)
                {
                    Log.Error($"Error while generating new Item Code : {ex.Message}");
                    GenerateItemCodeForBulkItem(itmList);
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException is SqlException)
                {
                    var innerExp = ex.InnerException as SqlException;
                    if (innerExp.Number is 2601)
                    {
                        Log.Error($"Error while generating new Item Code : {ex.Message}");
                        GenerateItemCodeForBulkItem(itmList);
                    }
                }
            }
        }
    }
}


//{
//    "itemName": "Noodles",
//    "itemCode": "Noodle_preeti",
//    "brandName": "Preeti",
//    "unitOfMeasurement": "packs",
//    "purchaseRate": 15,
//    "salesRate": 18,
//    "quantity": 400,
//    "expiryDate": "2024-07-15T10:50:14.152Z",
//    "storeName": "SuperMart"
//  },

