using InventoryMgmt.CustomException;
using InventoryMgmt.DataAccess;
using InventoryMgmt.Migrations;
using InventoryMgmt.Model;
using InventoryMgmt.Model.DTOs;
using Serilog;

namespace InventoryMgmt.Service
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;
        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsStockAvailable(AddSalesModel saleDTO)
        {
            StoreModel StoreFromServer = _context.stores.Where(s => s.storeId == saleDTO.StoreId && s.isActive == true).FirstOrDefault();
            if (StoreFromServer is null)
            {
                Log.Error("Store Not Found in the server while searching for Sales Of Item");
                return false;
            }
            ItemModel ItemFromServer = _context.items.Where(i => i.ItemId == saleDTO.ItemId && i.IsActive == true).FirstOrDefault();
            if (ItemFromServer is null)
            {
                Log.Error("Item Not Found in the server while searching for Sales Of Item");
                return false;           
            }

            StockModel ServerStock = _context.stocks.Where(s => s.storeId.Equals(saleDTO.StoreId) && s.itemId.Equals(saleDTO.ItemId)).FirstOrDefault();
            if (ServerStock is null && ServerStock.quantity < saleDTO.Quantity && ServerStock.expiryDate < DateTime.Now)
            {
                return false;
            }
            return true;

        }
        public bool StockManager(AddSalesModel saleDTO)
        {
            //Queue<AddSalesModel> q = new Queue<AddSalesModel>();
            //q.Enqueue(saleDTO);
            //foreach (var sales in q)
            //{
                StockModel StockFromServer = _context.stocks.Where(s => s.itemId == saleDTO.ItemId && s.storeId == saleDTO.StoreId).FirstOrDefault();

                decimal RemainingQuantity = StockFromServer.quantity - saleDTO.Quantity;
                if (RemainingQuantity < 0)
                {
                    throw new NegativeQuantityException("Quantity Cannot be in Negative");
                }
                StockFromServer.quantity = RemainingQuantity;
            //}
            return true;
        }
    }
}
