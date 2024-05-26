using InventoryMgmt.Model.DTOs;

namespace InventoryMgmt.Service
{
    public interface ISalesService
    {
        bool SellItem(AddSalesModel saleDTO);
    }
}