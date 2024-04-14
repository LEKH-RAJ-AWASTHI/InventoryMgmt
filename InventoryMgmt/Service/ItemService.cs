using InventoryMgmt.DataAccess;
using InventoryMgmt.EntityValidations;
using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;
using System.Security.Cryptography.X509Certificates;

namespace InventoryMgmt.Service
{
    public class ItemService : IItemService
    {
        ApplicationDbContext db = new ApplicationDbContext();
        ItemModel itemModel = new ItemModel();
        StockModel stockModel = new StockModel();

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

            ValidateItem validateItem = new ValidateItem();

            if (itemCode is "" && itemName is "" && brandName is "" && unitOfMeasurement is "" && storeName is "")
            {
                throw new InvalidOperationException($"All fields are required");
            }
            if (purchaseRate is 0 && salesRate is 0 && quantity is 0)
            {
                throw new InvalidOperationException($"The fields (Purchase Rate, Sales Rate, Quantity) cannot be zero");
            }
            StoreModel serverStoreModel = db.stores.Where(s => s.storeName == storeName && s.isActive == true).FirstOrDefault();
            if (serverStoreModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. Store not added or deleted. You first have to add new Store");
            }
            ItemModel serverItemModel = db.items.Where(i => i.ItemCode == itemCode).FirstOrDefault();
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
            db.items.Add(itemModel);
            db.SaveChanges();
            serverItemModel = db.items.Where(i => i.ItemCode == itemCode && i.IsActive == true).FirstOrDefault();
            if (serverItemModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. ");

            }

            //Mapping StockModel
            stockModel.storeId = serverStoreModel.storeId;
            stockModel.itemId = serverItemModel.ItemId;
            stockModel.quantity = quantity;
            stockModel.expiryDate = expiryDate;
            db.stocks.Add(stockModel);
            db.SaveChanges();
            return true;
        }
        public bool Update(int itemId, ItemFormModel item)
        {
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
            if (itemName is "" && brandName is "" && unitOfMeasurement is "")
            {
                throw new InvalidOperationException($"All fields are required");
            }
            if (purchaseRate is 0 && salesRate is 0 && quantity is 0)
            {
                throw new InvalidOperationException($"The fields (Purchase Rate, Sales Rate, Quantity) cannot be zero");
            }
            ItemModel serverItemModel = db.items.Where(i => i.ItemId == itemId).FirstOrDefault();
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
            StockModel serverStockModel = db.stocks.Where(s => s.itemId == serverItemModel.ItemId).FirstOrDefault();
            if (serverStockModel is null)
            {
                return false;
            }
            serverStockModel.quantity = quantity;
            serverStockModel.expiryDate = expiryDate;
            db.SaveChanges();

            return true;
        }
        public bool ChangeItemActiveStatus(int itemId)
        {
            if (itemId is 0)
            {
                throw new InvalidOperationException($"ItemId cannot be zero");
            }

            ItemModel serverItemModel = db.items.Where(i => i.ItemId == itemId).FirstOrDefault();
            


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

            serverItemModel.IsActive = !serverItemModel.IsActive;

            //StockModel serverStockModel = db.stocks.Where(s => s.itemId == serverItemModel.ItemId).FirstOrDefault();
            //if (serverStockModel is null)
            //{
            //    return false;
            //}
            //serverStockModel.quantity = 0;
            db.SaveChanges();
            return true;
        }
        public ItemModelClass Get(int itemId)
        {
            if (itemId is 0)
            {
                throw new InvalidOperationException($"ItemId cannot be zero");
            }

            ItemModel serverItemModel = db.items.Where(i => i.ItemId == itemId).FirstOrDefault();

            if (serverItemModel is null)
            {
                throw new InvalidOperationException($"Cannot find matching detail. ");
            }

            return serverItemModel;
        }
        public IEnumerable<ItemModel> GetAll()
        {
            return db.items.Where(i => i.IsActive == true).ToList<ItemModel>();
        }
    }
}

