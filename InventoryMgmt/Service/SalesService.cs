using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Model.DTOs;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace InventoryMgmt.Service
{
    public class SalesService : ISalesService
    {
        private readonly IStockService _stockService;
        private readonly ApplicationDbContext _context;
        public SalesService(ApplicationDbContext context, IStockService service)
        {
            _context = context;
            _stockService = service;
        }
        public bool SellItem(AddSalesModel saleDTO)
        {
            try
            {
                ItemModel? ItemFromServer = _context.items.Where(i => i.ItemId == saleDTO.ItemId && i.IsActive == true).FirstOrDefault();
                if (_stockService.IsStockAvailable(saleDTO))
                {
                    using (var context = _context.Database.BeginTransaction())
                    {
                        SalesModel salesModel = new SalesModel();
                        salesModel.Quantity = saleDTO.Quantity;
                        salesModel.dateTime = DateTime.Now;
                        salesModel.itemId = saleDTO.ItemId;
                        salesModel.SalesPrice = ItemFromServer.SalesRate;
                        salesModel.TotalPrice = saleDTO.Quantity * ItemFromServer.SalesRate;
                        salesModel.storeId = saleDTO.StoreId;

                        _context.sales.Add(salesModel);

                        bool response = _stockService.StockManager(saleDTO);
                        if (response)
                        {
                            _context.SaveChanges();
                        }
                        else
                        {
                            throw new Exception("Unable to deduct Item quantity from stock");
                        }
                        context.Commit();
                        return true;
                    }


                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured while selling Item " + ex.Message);
                return false;
            }
        }

    }
}
