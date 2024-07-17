using Isatays.FTGO.CustomerService.Core.Entities;
using KDS.Primitives.FluentResult;

namespace Isatays.FTGO.CustomerService.Core.Interfaces;

public interface ICustomerService
{
    Task<Result<Customer?>> VerifyCustomer(int id, string name, string email, string phoneNumber);
}
