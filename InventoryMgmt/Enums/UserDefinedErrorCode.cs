namespace InventoryMgmt
{
    public class UserDefinedErrorCode
    {
        public enum ErrorCode
        {
            InsertItemError= 1000,
            UpdateItemError= 1001,
            GetItemError= 1002
        }
    }
     public static class SessionVariableEnum
    {
        public static readonly string CurrentUser = "currentUser";
    }
    public static class NotificationVariableEnum
    {
        public static readonly string LowStock = "LowInventory";
        public static readonly string MileStoneSales = "MileStoneSales";
    }
}
