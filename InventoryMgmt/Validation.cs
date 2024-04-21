using InventoryMgmt.Model;
using System.Data;

namespace InventoryMgmt
{
    public class Validation : IValidation
    {
        public string RegisterUserValidation(RegisterUserModel user)
        {
            if (user == null)
            {
                return $"{nameof(user)} is required to Login!";
            }
            if (user.username.Length is 0 && user.fullName.Length is 0 && user.password.Length is 0 && user.role.Length is 0)
            {

                return "All fields are required";
            }
            if (user.password.Length is < 4)
            {

                return "Passoword length should be minimum 4";
            }

            // Role Can be either "Admin" OR "User"

            if (user.role is not "Admin" && user.role is not "User")
            {

                return "Role Can be either Admin OR User";
            }

            return "";
        }

    }
}
