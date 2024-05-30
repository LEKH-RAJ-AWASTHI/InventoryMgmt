using InventoryMgmt.DataAccess;
using InventoryMgmt.Model;
using InventoryMgmt.Model.DTOs;
using InventoryMgmt.Service;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        ApplicationDbContext _context;
        ISalesService _salesService;
        public SalesController(ApplicationDbContext context, ISalesService salesService)
        {
            _context = context;
            _salesService = salesService;
        }

        // GET: api/<SalesController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // POST api/<SalesController>
        [HttpPost]
        public IActionResult Post([FromBody] AddSalesModel salesDTO)
        {
            try
            {

                if (salesDTO == null)
                {
                    Log.Error("SalesDTO inside the Sales Controller is Null");
                    throw new ArgumentNullException("Adding sales model is null");

                }
                else
                {
                    bool response = _salesService.SellItem(salesDTO);
                    if (response)
                    {
                        return Ok("Sale of Item successfull");
                    }
                    else
                    {
                        return BadRequest();
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Exception occured while selling item" + ex.Message);
                return BadRequest();
            }

        }


    }
}
