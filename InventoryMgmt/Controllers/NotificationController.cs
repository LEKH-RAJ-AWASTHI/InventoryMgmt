using InventoryMgmt.DataAccess;
using InventoryMgmt.Hubs;
using InventoryMgmt.Model;
using InventoryMgmt.Model.DTOs;
using InventoryMgmt.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Serilog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InventoryMgmt.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IHubContext<InventoryHub> _hubContext;
        private readonly INotificationService _notificationService;
        public NotificationController(IHubContext<InventoryHub> hubContext, INotificationService notification) 
        { 
            _notificationService = notification;
            _hubContext = hubContext;
        }
        // GET: api/<ValuesController>
        // GET api/<ValuesController>/5
        [HttpGet]
        public IActionResult GetLowStockNotfication()
        {
            _notificationService.LowStockMessage();

            return Ok();
        }

        // for now we don't need Milestone sales notification in api
        // [HttpGet]
        // public IActionResult GetMileStoneSalesNotification()
        // {
        //     _notificationService.MileStoneSalesMessage();
        // }
       

    }
}
