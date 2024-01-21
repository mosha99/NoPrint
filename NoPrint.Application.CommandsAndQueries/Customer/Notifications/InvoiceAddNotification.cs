using MediatR;
using NoPrint.Identity.Share;


namespace NoPrint.Application.CommandsAndQueries.Customer.Notifications;

public class InvoiceAddNotification : INotification
{
    public InvoiceAddNotification(InvoicesId invoiceId, decimal invoiceAmount, CustomerId customerId)
    {
        InvoiceId = invoiceId;
        InvoiceAmount = invoiceAmount;
        CustomerId = customerId;
    }

    public InvoicesId InvoiceId { get; set; }
    public decimal InvoiceAmount { get; set; }
    public CustomerId CustomerId { get; set; }
}