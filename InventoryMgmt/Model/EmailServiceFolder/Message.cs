using MimeKit;

namespace InventoryMgmt;

public class Message
{
    public List<MailboxAddress> To { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public Message(SendEmailModel sendEmailModel)
    {
        To = sendEmailModel.To;
        Subject= sendEmailModel.Subject;
        Content= sendEmailModel.Content;
    }
}
