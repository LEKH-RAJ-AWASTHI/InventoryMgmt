using InventoryMgmt.Model;

namespace InventoryMgmt;

public interface IEmailService
{
    void LowStockEmailService(ItemModel itemModel, decimal quantity);
    void MilestoneItemSaleEmailService(ItemModel itemModel , decimal quantity);
}
