using InventoryMgmt.Model;
using InventoryMgmt.Service.Service_Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{
    [Authorize (Policy ="AdminOnly")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            try
            {
                if (_userService.GetAllUser() == null)
                {
                    return Ok(Enumerable.Empty<UserModel>());
                }
                return Ok(_userService.GetAllUser());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{username}")]
        public IActionResult Get(string username)
        {
            if (_userService.GetUserByUsername(username) == null)
            {
                return Ok("User Not found");
            }
                
            return Ok(_userService.GetUserByUsername(username));
        }



        // PUT api/<UserController>/5
        [HttpPut("Update Password")]
        public IActionResult Put(LoginModel login, string newPwd)
        {
            string error = _userService.UpdateuserPassword(login, newPwd);
            if(error.Length is 0)
            {
                return Ok("Password Updated Successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, error);
            }
        }

        // DELETE api/<UserController>/5
        [HttpPut("ChangeUserActiveStatus")]
        public IActionResult ChangeUserActiveStatus(LoginModel login)
        {
            try
            {
                if (login is null)
                {
                    throw new ArgumentNullException($"{nameof(login)} is required!");
                }
                bool response = _userService.ChangeUserActiveStatus(login);
                if (response)
                {
                    return StatusCode(StatusCodes.Status200OK, "User is successfully deleted!");
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound, "User Not Found in the Server");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status404NotFound, $"User Not Found in the Server. Exception {ex}");
            }
        }
    }
}
