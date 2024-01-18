using NoPrint.Application.CommandsAndQueries.CommandValidator;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.CommandsAndQueries.Customer.Validators;

public class FillCustomerCommandValidator : ICommandValidator<FillCustomerCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICustomerRepository _customerRepository;

    public FillCustomerCommandValidator(IUserRepository userRepository, ICustomerRepository customerRepository)
    {
        _userRepository = userRepository;
        _customerRepository = customerRepository;
        Errors = new List<LogicalError>();
    }

    public List<LogicalError> Errors { get; set; }

    public async Task Validate(FillCustomerCommand command)
    {
        command.CustomerName.Validate(x => x?.Length >= 3)
            .WithName(nameof(command.CustomerName))
            .WithError("Error_Length_Min_3")
            .AddToList(Errors);

        command.CustomerAddress.Validate(x => !string.IsNullOrWhiteSpace(x))
            .WithName(nameof(command.CustomerAddress))
            .WithError("Error_Required")
            .AddToList(Errors);

        command.UserName.Validate(x => x?.Length >= 5)
            .WithName(nameof(command.UserName))
            .WithError("Error_Length_Min_5")
            .AddToList(Errors);

        //Todo validate Password high level
        command.Password.Validate(x => x?.Length >= 5)
            .WithName(nameof(command.Password))
            .WithError("Error_Length_Min_5")
            .AddToList(Errors);

        if (!Errors.Any())
        {

            await command.UserName.ValidateAsync(async x => !await _userRepository.AnyExistWithThisUsername(x))
                 .WithName(nameof(command.UserName))
                 .WithError("Error_UserNameUniq")
                 .AddToListAsync(Errors);


            await command.GetCustomerId()
                .ValidateAsync(async x => await FindCustomer(x))
                 .WithName(nameof(command.CustomerId))
                 .WithError("Error_CustomerNotFind")
                 .AddToListAsync(Errors);

        }

        if (Errors.Any()) throw new AggregateInvalidPropertyException(Errors);
    }

    public async Task<bool> FindCustomer(CustomerId customerId)
    {
        try
        {
            var customer = await _customerRepository.GetByIdAsync(customerId);
            return customer is not null;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}