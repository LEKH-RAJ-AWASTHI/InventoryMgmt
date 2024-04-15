using InventoryMgmt.DataAccess;
using InventoryMgmt.EntityValidations;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;
using System.Security.Cryptography.X509Certificates;

namespace InventoryMgmt.Service
{
    public class ItemService : IItemService
    {
        private readonly ApplicationDbContext _context;
        ItemModel itemModel = new ItemModel();
        StockModel stockModel = new StockModel();
        public ItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddItem(AddItemFormModel item)
        {
            string itemCode = item.itemCode;
            string itemName = item.itemName;
            string brandName = item.brandName;
            string unitOfMeasurement = item.unitOfMeasurement;
            decimal purchaseRate = item.purchaseRate;
            decimal salesRate = item.salesRate;
            decimal quantity = item.quantity;
            DateTime expiryDate = item.expiryDate;
            string storeName = item.storeName;

            //all of below commented validations are replaced by Fluent valdation   

            //ValidateItem validateItem = new ValidateItem();

            //if (itemCode is "" && itemName is "" && brandName is "" && unitOfMeasurement is "" && storeName is "")
            //{
            //    throw new InvalidOperationException($"All fields are required");
            //}
            //if (purchaseRate is 0 && salesRate is 0 && quantity is 0)
            //{
            //    throw new InvalidOperationException($"The fields (Purchase Rate, Sales Rate, Quantity) cannot be zero");
            //}
            StoreModel serverStoreModel = _context.stores.Where(s => s.storeName == storeName && s.isActive == true).FirstOrDefault();
            if (serverStoreModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. Store not added or deleted. You first have to add new Store");
            }
            ItemModel serverItemModel = _context.items.Where(i => i.ItemCode == itemCode).FirstOrDefault();
            if (serverItemModel is not null)
            {
                throw new InvalidDataException($"Item code already exist. Please use another item code");
            }

            //Mapping ItemModel 
            itemModel.ItemCode = itemCode;
            itemModel.ItemName = itemName;
            itemModel.BrandName = brandName;
            itemModel.PurchaseRate = purchaseRate;
            itemModel.SalesRate = salesRate;
            itemModel.UnitOfMeasurement = unitOfMeasurement;
            itemModel.IsActive = true;
            _context.items.Add(itemModel);
            _context.SaveChanges();
            serverItemModel = _context.items.Where(i => i.ItemCode == itemCode && i.IsActive == true).FirstOrDefault();
            if (serverItemModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. ");

            }

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
            string itemName = item.itemName;
            string itemCode = item.itemCode;
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
            if (itemId is 0)
            {
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
            if (itemId is 0)
            {
                throw new InvalidOperationException($"ItemId cannot be zero");
            }

            ItemModel serverItemModel = _context.items.Where(i => i.ItemId == itemId).FirstOrDefault();

            if (serverItemModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. ");
            }

            return serverItemModel;
        }
        public IEnumerable<ItemModel> GetAll()
        {
            return _context.items.Where(i => i.IsActive == true).ToList<ItemModel>();
        }
    }
}

