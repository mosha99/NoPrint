using MediatR;
using NoPrint.Application.CommandsAndQueries.Customer.Notifications;
using NoPrint.Application.Infra;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Notification.Share;

namespace NoPrint.Application.Services.Handlers;

public class InvoiceAddNotificationHandlers : INotificationHandler<InvoiceAddNotification>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMessageSenderService _messageSenderService;
    private readonly IConfigurationGetter _configurationGetter;

    public InvoiceAddNotificationHandlers(ICustomerRepository customerRepository, IMessageSenderService messageSenderService ,IConfigurationGetter configurationGetter)
    {
        _customerRepository = customerRepository;
        _messageSenderService = messageSenderService;
        _configurationGetter = configurationGetter;
    }

    public async Task Handle(InvoiceAddNotification notification, CancellationToken cancellationToken)
    {
        var customer = await _customerRepository.GetByIdAsync(notification.CustomerId);

        var url = $"{_configurationGetter.GetUrl()}/{customer._Id}/{notification.InvoiceId.Id}";

        var message = string.Format("Message_InvoiceAdd",customer.GetNameOrPhone(),notification.InvoiceAmount,url);

        await _messageSenderService.Send(customer.PhoneNumber,message);
    }
}