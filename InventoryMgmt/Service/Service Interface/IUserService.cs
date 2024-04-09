using InventoryMgmt.Model;

namespace InventoryMgmt.Service.Service_Interface
{
    public interface IUserService
    {
        bool Deleteuser(LoginModel login);
        List<UserModel> GetAllUser();
        string UpdateuserPassword(LoginModel model, string newPwd);
        UserModel GetUserByUsername(string username);
    }
}