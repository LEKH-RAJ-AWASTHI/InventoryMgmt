using InventoryMgmt.DataAccess;
using InventoryMgmt.Hubs;
using InventoryMgmt.Model;
using InventoryMgmt.Model.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace InventoryMgmt.Service
{
    public interface INotificationService
    {
        void LowStockMessage();
        void MileStoneSalesMessage(AddSalesModel saleDTO);
        void AddInventoryMessage(int itemId, decimal quantity);
        IQueryable<Notification> GetLatestNotification();

    }
}