using KDS.Primitives.FluentResult;
using MediatR;

namespace Isatays.FTGO.CustomerService.Core.Customers;

public class CheckCustomerCommand : IRequest<Result<string>>
{

}
