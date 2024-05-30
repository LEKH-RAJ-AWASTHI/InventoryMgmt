using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model
{
    public class RegisterUserModel : LoginModel
    {
        public string fullName { get; set; }
        public string role { get; set; }

        public string email { get; set; }
    }
}
