using System.Security.Cryptography.X509Certificates;
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
        private readonly IEmailSender _emailSender;
        private readonly IEmailService _emailService;

        private readonly IConfiguration _configuration;
        public NotificationService(IEmailService emailService, ApplicationDbContext context, IHubContext<InventoryHub> hubContext, IEmailSender emailSender, IConfiguration configuration)
        {
            _context = context;
            _hubContext = hubContext;
            _emailSender = emailSender;
            _configuration= configuration;
            _emailService= emailService;

        }
        public void LowStockMessage()
        {
            var inventoryItems = _context.stocks.ToList();
            foreach (var item in inventoryItems)
            {
                if (item.quantity < 50)
                {
                    ItemModel itemDetail = _context.items.Where(i => i.ItemId == item.itemId).FirstOrDefault();
                    var storeName =(from store in _context.stores join 
                                            stocks in _context.stocks on store.storeId equals stocks.storeId
                                            where stocks.itemId == item.itemId select store.storeName).FirstOrDefault();
       
                    HubMessageDTO hubMessage = new HubMessageDTO
                    {
                        Item = itemDetail.ItemName,
                        StoreName =storeName,
                        Quantity = item.quantity,
                    };
                    if (itemDetail != null)
                    {
                        _hubContext.Clients.All.SendAsync("GetLowStockNotification", hubMessage);
                        Log.Information($"Inventory update: {itemDetail.ItemName} - {item.quantity}");
                        //checking for if same email is sent
                        //1. By that particular ItemId
                        //2. If email is sent within 5 days or not,
                        //3. If IsSent is true or not
                        //4. Is the email is of the low stock message 
                        int day = DateTime.Now.Day -5;
                        var emailStatus= _context.emailLogs.Where(i=> i.ItemId== itemDetail.ItemId).OrderByDescending(e=> e.dateTime).FirstOrDefault();
                        if(emailStatus is not null)
                        {
                            if(emailStatus.dateTime.Day > day && emailStatus.Type == EmailLogAlertTypeEnum.QuantityLowStock)
                            {
                                return;
                            }
                        }
                        else
                        {
                            _emailService.LowStockEmailService(itemDetail, item.quantity);
                            SaveNotificationInDB(hubMessage);
                        }
                       
                        //if data comes in the emailStatus then the email is already sent and no need to send again if emailstatus is null then send email again.

                        
                    }

                }
            }
        }

        public void MileStoneSalesMessage(AddSalesModel saleDTO)
        {
            ItemModel maxSaleItem = _context.items.Where(i => i.ItemId == saleDTO.ItemId).FirstOrDefault();
            StoreModel maxSaleItemStore = _context.stores.Where(s => s.storeId == saleDTO.StoreId).FirstOrDefault();

            HubMessageDTO hubMessage = new HubMessageDTO
            {
                Item = maxSaleItem.ItemName,
                StoreName = maxSaleItemStore.storeName,
                Quantity = saleDTO.Quantity,
            };
            _hubContext.Clients.All.SendAsync("MileStoneSale", hubMessage);
            _emailService.MilestoneItemSaleEmailService(maxSaleItem, saleDTO.Quantity);
            SaveNotificationInDB(hubMessage);
            Log.Information($"Milestone sales : {maxSaleItem.ItemName} from store: {maxSaleItemStore.storeName} on date {DateTime.Now}");
        }

        public void AddInventoryMessage(int itemId, decimal quantity)
        {
            var stock = _context.stocks.Where(i=>i.itemId ==itemId).FirstOrDefault();
            ItemModel item = _context.items.Where(i=> i.ItemId== stock.itemId).FirstOrDefault();
            string store = _context.stores.Where(s=> s.storeId==stock.storeId).Select(s=> s.storeName).FirstOrDefault();
            HubMessageDTO hubMessageDTO = new HubMessageDTO
            {
                Item = item.ItemName,
                StoreName = store,
                Quantity = quantity
            };

            _hubContext.Clients.All.SendAsync("AddingItemToInventory",hubMessageDTO);
            _emailService.AddItemInventoryEmailService(item, quantity, store);
        }
        public IQueryable<Notification> GetLatestNotification()
        {
            IQueryable<Notification> notification = _context.notifications.OrderByDescending(n=> n.Date).Take(10);
            return notification;
        }

        private void SaveNotificationInDB(HubMessageDTO hubMessage)
        {
            Notification notification = new Notification
            {
                Item = hubMessage.Item,
                Quantity = hubMessage.Quantity,
                StoreName = hubMessage.StoreName
            };
            _context.Add(notification);
        }


    }
}
