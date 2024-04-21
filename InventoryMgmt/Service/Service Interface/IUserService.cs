using InventoryMgmt.Model;

namespace InventoryMgmt.Service.Service_Interface
{
    public interface IUserService
    {
        bool ChangeUserActiveStatus(LoginModel login);
        List<UserModel> GetAllUser();
        string UpdateuserPassword(LoginModel model, string newPwd);
        UserModel GetUserByUsername(string username);
    }
}