using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;

namespace InventoryMgmt.Service
{
    public class NotificationService : INotificationService
    {
        private readonly ApplicationDbContext _context;
        public NotificationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Notification GetNotification()
        {
            var productBelowThreshold = _context.stocks.Where(i => i.quantity < 50).FirstOrDefault();
            if (productBelowThreshold is not null)
            {
                var item = _context.items.Where(i => i.ItemId == productBelowThreshold.itemId).FirstOrDefault();
                if (item is not null)
                {
                    return new Notification
                    {
                        NotificationTitle = $"{item.ItemName} is low in Stock",
                        Message = $"{item.ItemName} has {productBelowThreshold.quantity} quantity in stock. Please order this Item"
                    };
                }
                else
                {
                    throw new Exception("ItemNot Found");

                }

            }
            else
            {
                throw new Exception("No Product below Threshold");
            }

        }
    }
}
