using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Service.Service_Interface;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace InventoryMgmt.Service
{
    
    public class UserService : IUserService
    {
        ApplicationDbContext _dbContext = new ApplicationDbContext();
        public string UpdateuserPassword(LoginModel loginModel, string newPwd) 
        { 
            var userFromServer= _dbContext.users.Where(u=> u.username==loginModel.username && u.password == loginModel.password).FirstOrDefault();
            if (userFromServer==null)
            {
                return "username and password required";
            }
            if(newPwd == null)
            {
                return "New Password is required";
            }
            if(newPwd.Length is < 4)
            {
                return "Password Length must be atleast 4";
            }

            userFromServer.password = newPwd;
            _dbContext.SaveChanges();
            return "";
        }
        public bool ChangeUserActiveStatus(LoginModel loginModel)
        {
                var userFromServer = _dbContext.users.Where(u => u.username == loginModel.username && u.password == loginModel.password).FirstOrDefault();
                if (userFromServer == null)
                {
                    throw new InvalidOperationException($"Cannot find matching detail");
                }
                userFromServer.isActive = false;
                _dbContext.SaveChanges();
                return true;
        }
        public List<UserModel> GetAllUser()
        { 
            return _dbContext.users.Where(u=> u.isActive==true).ToList();
        }
        public UserModel GetUserByUsername(string username)
        {
            var userFromServer = _dbContext.users.Where(u => u.username == username && u.isActive == true ).FirstOrDefault();
            if (userFromServer == null)
            {
                throw new InvalidOperationException($"Cannot find matching detail");
            }
            return userFromServer;
        }
    }
}
