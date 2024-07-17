using Isatays.FTGO.CustomerService.Core.Entities;
using KDS.Primitives.FluentResult;
using MediatR;

namespace Isatays.FTGO.CustomerService.Core.Customers;

public class VerifyCustomerCommand : IRequest<Result<Customer?>>
{
	public VerifyCustomerCommand(int id, string name, string email, string phoneNumber)
	{
		Id = id;
		Name = name;
		Email = email;
		PhoneNumber = phoneNumber;
	}

	public int Id { get; init; }

	public string Name { get; init; }

	public string Email { get; init; }
	
	public string PhoneNumber { get; init; }
}
