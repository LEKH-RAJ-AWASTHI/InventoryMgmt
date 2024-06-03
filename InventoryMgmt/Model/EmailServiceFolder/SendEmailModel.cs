using Common;
using InventoryMgmt.Service;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using MimeKit;
using Serilog;

namespace InventoryMgmt;

public class SendEmailModel 
{
    public List<MailboxAddress> To = new List<MailboxAddress>();
    public string Subject {get; set;}

    public string Content {get; set; }

    public SendEmailModel(IConfiguration configuration, string subject, string content)
    {
        string? emails= configuration.GetValue<string>("ClientEmail:To");
        string[] emailArray= emails.Split(";");
        To.AddRange(emailArray.Select(x=> new MailboxAddress("email", x)));   
        Subject= subject;
        Content =content;
    }
}

