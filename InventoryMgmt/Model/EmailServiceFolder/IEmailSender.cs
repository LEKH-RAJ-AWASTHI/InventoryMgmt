namespace InventoryMgmt;

public interface IEmailSender
{
    public void SendEmail(Message message);
}
