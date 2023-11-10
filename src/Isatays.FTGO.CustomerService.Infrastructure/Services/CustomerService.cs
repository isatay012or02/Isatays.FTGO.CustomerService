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

	public async Task<Result<bool>> VerifyCustomer(Guid id, string name, string email)
	{
		var result = await _dataContext
							.Customers
							.Where(c => c.Id == id
							&& c.Name == name
							&& c.Email == email)
							.FirstOrDefaultAsync();

		return Result.Success(result.IsAvailable);
	}
}
