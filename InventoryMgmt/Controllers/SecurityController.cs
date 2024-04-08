using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        private readonly IConfiguration _configuration;
        public SecurityController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/<SecurityController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<SecurityController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SecurityController>
        [HttpPost]
        public IActionResult Post([FromBody] UserModel user)
        {
            if (user is null)
            {
                throw new ArgumentNullException($"{nameof(user)} is required to Login!");
            }

            string uname = user.username;
            string pwd = user.password;
            string role = user.role;
            var userFromServer = db.users
                                   .Where(u => u.username == uname && u.password == pwd && u.role== role ).FirstOrDefault();

            //UserModel u = (from temp in db.users where temp.username == uname && temp.password == pwd select temp).FirstOrDefault();
            if (userFromServer is null)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, "Wrong Credentials, Please try again");
            }
            JwtToken token = new JwtToken(_configuration);
            String t= token.GeneratedToken(uname, role);
            
            
            return Ok(t);

        }

    }
}
