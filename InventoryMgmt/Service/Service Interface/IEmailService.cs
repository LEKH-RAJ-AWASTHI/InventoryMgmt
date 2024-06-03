using InventoryMgmt.Model;

namespace InventoryMgmt;

public interface IEmailService
{
    void LowStockEmailService(int itemId,string itemName, decimal quantity);
    void MilestoneItemSaleEmailService(ItemModel itemModel , decimal quantity);
    void AddItemInventoryEmailService(ItemModel itemModel, decimal quantity, string store);
}
