using InventoryMgmt.Hubs;
using InventoryMgmt.Model;
using InventoryMgmt.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

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
        public IActionResult GetProductNotification(int id)
        {
            Notification notification = _notificationService.GetNotification();
            _hubContext.Clients.All.SendAsync("signalr", "Hello World");
            return Ok(notification);
        }

    }
}
