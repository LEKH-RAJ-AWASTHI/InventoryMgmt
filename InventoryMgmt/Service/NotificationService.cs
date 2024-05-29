using InventoryMgmt.DataAccess;
using InventoryMgmt.Hubs;
using InventoryMgmt.Model;
using InventoryMgmt.Model.DTOs;
using Microsoft.AspNetCore.SignalR;
using Serilog;

namespace InventoryMgmt.Service
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<InventoryHub> _hubContext;
        public NotificationService(ApplicationDbContext context, IHubContext<InventoryHub> hubContext)
        {
            _context = context;
            _hubContext= hubContext;
        }
        public void LowStockMessage()
        {
            var inventoryItems = _context.stocks.ToList();
            foreach (var item in inventoryItems)
            {
                if (item.quantity < 50)
                {
                    ItemModel itemDetail = _context.items.Where(i => i.ItemId == item.itemId).FirstOrDefault();
                    if (itemDetail != null)
                    {
                        _hubContext.Clients.All.SendAsync("ReceiveInventoryUpdate", DateTime.Now, itemDetail.ItemName, item.quantity);
                        Log.Information($"Inventory update: {itemDetail.ItemName} - {item.quantity}");
                    }

                }
            }
        }
        public void MileStoneSalesMessage(AddSalesModel saleDTO)
        {
            ItemModel maxSaleItem = _context.items.Where(i=> i.ItemId == saleDTO.ItemId).FirstOrDefault();
            StoreModel maxSaleItemStore = _context.stores.Where(s=>s.storeId == saleDTO.StoreId).FirstOrDefault();
            
            HubMessageDTO hubMessage = new HubMessageDTO
            {
                Item= maxSaleItem.ItemName,
                StoreName = maxSaleItemStore.storeName,
                Quantity= saleDTO.Quantity,
                Date= DateTime.Now
            };
            _hubContext.Clients.All.SendAsync("MileStoneSales", hubMessage);
            Log.Information($"Milestone sales : {maxSaleItem.ItemName} from store: {maxSaleItemStore.storeName} on date {DateTime.Now}");
        }
    }
}
