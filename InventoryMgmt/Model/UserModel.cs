using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace InventoryMgmt.Model
{
    //tbl_users(userId, fullName, username, password, role, isActive)
    public class UserModel 
    {
        [Key]
        public Guid userId {  get; set; }
        [Required]
        public string fullName { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
        [Required]
        public string role { get; set; }
        [Required]
        public bool isActive { get; set; }
    }
}
