using InventoryMgmt.Model;
using InventoryMgmt.Service.Service_Interface;

namespace InventoryMgmt.Service
{
    public class UserService : IUserService
    {
        public void Adduser() { }
        public void UpdateuserPassword() { }
        public void Deleteuser() { }
        public List<UserModel> GetAllUser()
        {
            return new List<UserModel>();
        }

    }
}
