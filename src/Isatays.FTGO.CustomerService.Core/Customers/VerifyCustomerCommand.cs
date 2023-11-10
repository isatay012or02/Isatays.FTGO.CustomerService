using KDS.Primitives.FluentResult;
using MediatR;

namespace Isatays.FTGO.CustomerService.Core.Customers;

public class VerifyCustomerCommand : IRequest<Result<bool>>
{
	public VerifyCustomerCommand(Guid id, string name, string email)
	{
		Id = id;
		Name = name;
		Email = email;
	}

	public Guid Id { get; init; }

	public string Name { get; init; }

	public string Email { get; init; }
}
