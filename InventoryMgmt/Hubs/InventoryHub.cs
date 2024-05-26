using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Service;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel;
namespace InventoryMgmt.Hubs;

public class InventoryHub : Hub
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly ILogger<InventoryHub> _logger;
    private readonly ApplicationDbContext _context;
    public InventoryHub(IServiceScopeFactory scopeFactory, ILogger<InventoryHub> logger)
    {
        _logger = logger;
        _scopeFactory = scopeFactory;
    }
    public async Task NotifyInventoryChange(string itemName, int quantity)
    {
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
                    if (itemDetail != null)
                    {
                        await hubContext.Clients.All.SendAsync("ReceiveInventoryUpdate", itemDetail.ItemName, item.quantity);
                        _logger.LogInformation($"Inventory update: {itemDetail.ItemName} - {item.quantity}");
                    }

                }
            }
        }
        await Clients.All.SendAsync("ReceiveInventoryUpdate", itemName, quantity);
    }
    
}
