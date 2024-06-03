using Humanizer;
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
        private INotificationService _notificationService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public StockService
        (ApplicationDbContext context,
        IServiceScopeFactory scopeFactory,
        INotificationService notificationService,
        IEmailSender emailSender,
        IConfiguration configuration,
        IEmailService emailService
        )
        {
            _context = context;
            _scopeFactory = scopeFactory;
            _notificationService = notificationService;
            _emailSender = emailSender;
            _configuration= configuration;
            _emailService =emailService;
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

            var ItemFromServer = _context.items.Where(s => s.ItemId == StockFromServer.itemId).FirstOrDefault();

            decimal RemainingQuantity = StockFromServer.quantity - saleDTO.Quantity;
            if (RemainingQuantity < 0)
            {
                throw new NegativeQuantityException("Quantity Cannot be in Negative");
            }
            //this if condition is triggred when threshold is just crossed 
            if(StockFromServer.quantity> 50 && RemainingQuantity<= 50)
            {
                _emailService.LowStockEmailService(ItemFromServer.ItemId,ItemFromServer.ItemName, RemainingQuantity);
            }
            StockFromServer.quantity = RemainingQuantity;
            //checking for milestone sales 
            decimal MaxSaleOfItem =0;
            var ListOfSaleOfItem = _context.sales.Where(s => s.itemId == saleDTO.ItemId).Select(s => s.Quantity).ToList();
            if(ListOfSaleOfItem is not null && ListOfSaleOfItem.Count()>0)
            {
                MaxSaleOfItem= ListOfSaleOfItem.Max();
            }
            if (saleDTO.Quantity > MaxSaleOfItem)
            {
                _notificationService.MileStoneSalesMessage(saleDTO);
            }

            _notificationService.LowStockMessage();
            return true;
        }

    }
}
