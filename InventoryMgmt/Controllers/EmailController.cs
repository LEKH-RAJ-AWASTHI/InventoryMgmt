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
    public class EmailController : ControllerBase
    {
        private readonly IEmailSender _sender;
        private readonly INotificationService _notificationService;
        public EmailController(IEmailSender emailSender, INotificationService notificationService)
        {
            _sender = emailSender;
            _notificationService= notificationService;
        }
        // GET: api/<ValuesController>
        // GET api/<ValuesController>/5
        [HttpPost]
        public IActionResult SendEmail()
        {
            Message message = new Message(
                new String[] { "lekhrajawasthi123@gmail.com" },
                "Inventory Test Email",
                "This is the content of email from Inventory Management System"
            );
            _sender.SendEmail(message);
            _notificationService.EmailSentNotification(message.Subject);

            return Ok("Email Sent Successfully");
        }

        // for now we don't need Milestone sales notification in api
        // [HttpGet]
        // public IActionResult GetMileStoneSalesNotification()
        // {
        //     _notificationService.MileStoneSalesMessage();
        // }


    }
}
