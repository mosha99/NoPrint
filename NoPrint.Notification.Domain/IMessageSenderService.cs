namespace NoPrint.Notification.Share
{
    public interface IMessageSenderService
    {
        public Task Send(string phoneNumber , string message);
    }
}
