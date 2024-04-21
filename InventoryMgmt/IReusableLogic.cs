
namespace InventoryMgmt
{
    public interface IReusableLogic
    {
        List<dynamic> ExecuteStoredProcedure(string storedProcedureName, Dictionary<string, object> parameters);
    }
}