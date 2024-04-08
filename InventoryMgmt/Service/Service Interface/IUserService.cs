using InventoryMgmt.Model;

namespace InventoryMgmt.Service.Service_Interface
{
    public interface IUserService
    {
        void Adduser();
        void Deleteuser();
        List<UserModel> GetAllUser();
        void UpdateuserPassword();
    }
}