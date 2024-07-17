using Isatays.FTGO.CustomerService.Core.Entities;
using Isatays.FTGO.CustomerService.Core.Interfaces;
using KDS.Primitives.FluentResult;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace Isatays.FTGO.CustomerService.Infrastructure.Services;

public class CustomerService : ICustomerService
{
    private readonly IDataContext _dataContext;

	public CustomerService(IDataContext dataContext)
	{
		_dataContext = dataContext;
	}

	public async Task<Result<Customer?>> VerifyCustomer(int id, string name, string email, string phoneNumber)
	{
		var result = await _dataContext
							.Customers
							.Where(c => c.CustomerId == id
							&& c.Name == name
							&& c.Email == email
							&& c.PhoneNumber == phoneNumber)
							.FirstOrDefaultAsync();

		return Result.Success(result);
	}
}
