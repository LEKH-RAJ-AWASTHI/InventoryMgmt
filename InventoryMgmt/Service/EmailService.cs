using InventoryMgmt.DataAccess;
using InventoryMgmt.Hubs;
using InventoryMgmt.Model;
using Microsoft.AspNetCore.SignalR;

namespace InventoryMgmt.Service
{
    public class EmailService : IEmailService
    {
        public IConfiguration _configuration;
        public ApplicationDbContext _context;
        public IHubContext<InventoryHub> _hubContext;
        public IEmailSender _emailSender;
        public EmailService(
            IConfiguration configuration,
            IHubContext<InventoryHub> hubContext,
            ApplicationDbContext context,
            IEmailSender emailSender
            )
        {
            _configuration= configuration;
            _context = context;
            _hubContext = hubContext;
            _emailSender = emailSender;
        }
        public void LowStockEmailService(ItemModel itemModel, decimal quantity)
        {
            string Subject= EmailSubjectEnum.QuantityLowStock;
            string Content =$"Inventory of {itemModel.ItemName} is low in stock./n/n Remaining Quantity is {quantity}. ";

            SendEmailModel sendEmailModel = new SendEmailModel(_configuration, Subject, Content);
            Message message = new Message
            (
                sendEmailModel
            );
            _emailSender.SendEmail(message);
            EmailSentNotification(message.Subject);
            AddEmailLogs(itemModel.ItemId);
            
        }

        public void MilestoneItemSaleEmailService(ItemModel itemModel , decimal quantity)
        {
             string userEmail = "lekhrajawasthi123@gmail.com";
            string Subject = EmailSubjectEnum.MileStoneSales;
            string Content=$"Congratulations,\n {itemModel.ItemName}, has sold today in the record quantity of {quantity}";

            SendEmailModel sendEmailModel = new SendEmailModel(_configuration, Subject, Content);
            Message message = new Message(
                sendEmailModel
            );
            _emailSender.SendEmail(message);
            EmailSentNotification(message.Subject);
            AddEmailLogs(itemModel.ItemId);
        }
        public void AddItemInventoryEmailService(ItemModel itemModel, decimal quantity, string store)
        {
            string Subject = EmailSubjectEnum.AddingItemToInventory;
            string Content =  $"Updated Stock of item\n \n {itemModel.ItemName} with quantity {quantity} in store {store}";
            SendEmailModel sendEmailModel= new SendEmailModel(_configuration, Subject, Content);
            Message message = new Message
            (
            sendEmailModel

            );
            _emailSender.SendEmail(message);
            EmailSentNotification(message.Subject);
            AddEmailLogs(itemModel.ItemId);
        }
        private void EmailSentNotification(String Subject)
        {
            
            _hubContext.Clients.All.SendAsync("EmailNotification", Subject);
        }

        private void AddEmailLogs(int itemId)
        {
             string[] ClientEmails = _configuration.GetValue<string>("ClientEmail:To").Split(";");
            List<EmailLogs> emailLogsList= new List<EmailLogs>();
            foreach(var email in ClientEmails)
            {
                EmailLogs emailLogs = new EmailLogs
                {
                    ItemId = itemId,
                    IsSent = true,
                    User = email,
                    Type = EmailLogAlertTypeEnum.QuantityLowStock
                };
                emailLogsList.Add(emailLogs);
            }
            _context.emailLogs.AddRange(emailLogsList);
        }
    }
}