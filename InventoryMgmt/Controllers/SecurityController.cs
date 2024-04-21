using FluentValidation;
using FluentValidation.Results;
using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Validation_Rules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        string role="";

        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly IValidator<RegisterUserModel> _validator;
        private readonly IValidator<LoginModel> _loginValidator;

        public SecurityController(
            IConfiguration configuration, 
        ApplicationDbContext context,
            IValidator<RegisterUserModel> validator,
            IValidator<LoginModel> loginValidator)
        {
            _configuration = configuration;
            _context = context;
            _validator = validator;
            _loginValidator = loginValidator;
        }
        // GET api/<SecurityController>/5  for user registration
        [Authorize(Policy = "AdminOnly")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} is required to Login!");
            }

            ValidationResult result = await _validator.ValidateAsync(user);

            string allMessages = result.ToString(",\n");
            if(allMessages.Length > 0)
            {
                throw new InvalidOperationException(allMessages);
            }

            UserModel u= new UserModel();
            u.username = user.username;
            u.fullName= user.fullName;
            u.password= user.password;
            u.role = user.role;
            u.isActive = true;

            string uname = user.username;
            string fullName = user.fullName;
            string pwd= user.password;
            string role = user.role;
            var userFromServer = _context.users
                                   .Where(u => u.username == uname).FirstOrDefault();

            //userfromserver is null means user is not present in the database so we now create a new user
            if (userFromServer is null)
            {
                //string error =_validation.RegisterUserValidation(user);
                //if (error.Length is not 0)
                //{
                //    return StatusCode(StatusCodes.Status500InternalServerError, error);
                //}
                    //call entity framework to save the data
                    _context.users.Add(u);
                    _context.SaveChanges();
                    return Ok("User Added Successfully");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Username already exist. Use another username");
            }

        }
        // POST api/<SecurityController> for user Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} is required to Login!");
            }

            ValidationResult result = await _loginValidator.ValidateAsync(user);
            string errorMessage = result.ToString("\n");
            if (errorMessage.Length > 0)
            {
                throw new InvalidOperationException(errorMessage);
            }

            var userFromServer = _context.users
                                   .Where(u => u.username == user.username && u.password==user.password).FirstOrDefault();

            //UserModel u = (from temp in _context.users where temp.username == uname && temp.password == pwd select temp).FirstOrDefault();
            if (userFromServer is null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Wrong Credentials, Please try again");
            }
            role = userFromServer.role;
            JwtToken token = new JwtToken(_configuration);
            String t= token.GeneratedToken(userFromServer.username, role);
            
            
            return Ok(t);

        }
    }
}
