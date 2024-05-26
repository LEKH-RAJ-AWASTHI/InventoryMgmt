using InventoryMgmt.CustomException;
using InventoryMgmt.DataAccess;
using InventoryMgmt.Hubs;
using InventoryMgmt.Model;
using InventoryMgmt.Model.DTOs;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace InventoryMgmt.Service
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<StockService> _logger;

        public StockService(ApplicationDbContext context, IServiceScopeFactory scopeFactory, ILogger<StockService> logger)
        {
            _context = context;
            _scopeFactory = scopeFactory;
            _logger = logger;
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
                // checking if the stock is running out of stock
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    var hubContext = scope.ServiceProvider.GetRequiredService<IHubContext<InventoryHub>>();

                    var inventoryItems = context.stocks.ToList();
                    foreach (var item in inventoryItems)
                    {
                        if (item.quantity < 50)
                        {
                            ItemModel itemDetail = context.items.Where(i => i.ItemId == item.itemId).FirstOrDefault();
                            if (itemDetail != null) { 

                                 hubContext.Clients.All.SendAsync("ReceiveInventoryUpdate", itemDetail.ItemName, item.quantity);
                                _logger.LogInformation($"Inventory update: {itemDetail.ItemName} - {item.quantity}");
                            }
                        }
                    }
                }

            //}
            return true;
        }
    }
}
