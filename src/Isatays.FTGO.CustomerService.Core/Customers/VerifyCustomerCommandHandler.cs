using Isatays.FTGO.CustomerService.Core.Common.Exceptions;
using Isatays.FTGO.CustomerService.Core.Entities;
using Isatays.FTGO.CustomerService.Core.Interfaces;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;

namespace Isatays.FTGO.CustomerService.Core.Customers;

public class VerifyCustomerCommandHandler : IRequestHandler<VerifyCustomerCommand, Result<Customer?>>
{
	private readonly IDataContext _dataContext;
	private readonly ILogger<VerifyCustomerCommandHandler> _logger;
	private readonly ICustomerService _customerService;

	public VerifyCustomerCommandHandler(IDataContext dataContext, 
		ILogger<VerifyCustomerCommandHandler> logger,
		ICustomerService customerService)
	{
		_dataContext = dataContext;
		_logger = logger;
		_customerService = customerService;
	}

	public async Task<Result<Customer?>> Handle(VerifyCustomerCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var result = await _customerService.VerifyCustomer(request.Id, request.Name, request.Email, request.PhoneNumber);
			
			return Result.Success(result.Value);
		}
		catch (DatabaseException ex)
		{
			_logger.LogError(ex.Message);
			return Result.Failure<Customer?>(new Error("", ex.Message));
		}
    }
}
