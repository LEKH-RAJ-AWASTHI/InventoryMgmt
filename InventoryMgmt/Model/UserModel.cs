using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model
{
    //tbl_users(userId, fullName, username, password, role, isActive)
    public class UserModel : RegisterUserModel
    {
        [Key]
        public int  userId {  get; set; }
        public bool isActive { get; set; }
    }
}
