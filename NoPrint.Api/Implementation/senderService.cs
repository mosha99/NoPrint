using NoPrint.Application.Infra;
using NoPrint.Notification.Share;

namespace NoPrint.Api.Implementation;

public class MessageSenderService : IMessageSenderService
{
    public async Task Send(string phoneNumber, string message)
    {
        Console.WriteLine(phoneNumber +"_"+ message);
    }
}