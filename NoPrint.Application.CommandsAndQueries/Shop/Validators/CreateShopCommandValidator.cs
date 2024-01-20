using NoPrint.Application.CommandsAndQueries.CommandValidator;
using NoPrint.Application.CommandsAndQueries.Customer.Commands;
using NoPrint.Application.CommandsAndQueries.Shop.Commands;
using NoPrint.Customers.Domain.Repository;
using NoPrint.Framework.Exceptions;
using NoPrint.Framework.Validation;
using NoPrint.Identity.Share;
using NoPrint.Users.Domain.Repository;

namespace NoPrint.Application.CommandsAndQueries.Shop.Validators;

public class CreateShopCommandValidator : ICommandValidator<CreateShopCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICustomerRepository _customerRepository;

    public CreateShopCommandValidator(IUserRepository userRepository , ICustomerRepository customerRepository)
    {
        _userRepository = userRepository;
        _customerRepository = customerRepository;
        Errors = new List<LogicalError>();
    }

    public List<LogicalError> Errors { get; set; }

    public async Task Validate(CreateShopCommand command)
    {

        command.ShopName.Validate( x => x?.Length >= 3)
            .WithError("Error_Length_Min_3")
            .WithName(nameof(command.ShopName));

        command.PhoneNumber.Validate( x => x?.Length == 11)
            .WithError("Error_Length_Eql_11")
            .WithName(nameof(command.PhoneNumber));

        command.ShopAddress.Validate(x => !string.IsNullOrWhiteSpace(x))
            .WithError("Error_Required")
            .WithName(nameof(command.ShopAddress));

        command.UserName.Validate(x => x?.Length >= 5)
            .WithName(nameof(command.UserName))
            .WithError("Error_Length_Min_5")
            .AddToList(Errors);

        //Todo validate Password high level
        command.Password.Validate(x => x?.Length >= 5)
            .WithName(nameof(command.Password))
            .WithError("Error_Length_Min_5")
            .AddToList(Errors);

        if (Errors.Any()) throw new AggregateInvalidPropertyException(Errors);


        await command.UserName.ValidateAsync(async x => !await _userRepository.AnyExistWithThisUsername(x))
             .WithName(nameof(command.UserName))
             .WithError("Error_UserNameUniq")
             .AddToListAsync(Errors);



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