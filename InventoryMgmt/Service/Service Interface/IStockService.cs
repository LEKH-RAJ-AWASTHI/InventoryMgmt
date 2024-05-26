using InventoryMgmt.Model.DTOs;

namespace InventoryMgmt.Service
{
    public interface IStockService
    {
        bool IsStockAvailable(AddSalesModel saleDTO);
        bool StockManager(AddSalesModel saleDTO);
    }
}