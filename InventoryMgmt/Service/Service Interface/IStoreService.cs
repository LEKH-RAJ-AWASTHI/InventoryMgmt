﻿using InventoryMgmt.Model;
using InventoryMgmt.Model.ApiUseModel;

namespace InventoryMgmt.Service
{
    public interface IStoreService
    {
        bool AddStore(string storeName);
        bool ChangeStoreActiveStatus(string storeName);
        List<StoreRegisterModel> ShowAllStores();
        bool UpdateStore(string oldStoreName, string newStoreName);
        List<dynamic> showStockLevel();
        //bool SalesOfItem(int StoreId, int itemId, decimal Quantity);
    }
}