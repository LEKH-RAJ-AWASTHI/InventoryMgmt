using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model
{
    public class RegisterUserModel
    {
        public int userId { get; set; }

        public string fullName { get; set; }

        public string username { get; set; }

        public string password { get; set; }
        public string role { get; set; }
        public bool isAdmin { get; set; }
    }
}
