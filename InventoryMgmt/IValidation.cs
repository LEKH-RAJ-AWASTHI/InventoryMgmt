using InventoryMgmt.Model;

namespace InventoryMgmt
{
    public interface IValidation
    {
        string RegisterUserValidation(RegisterUserModel user);
    }
}