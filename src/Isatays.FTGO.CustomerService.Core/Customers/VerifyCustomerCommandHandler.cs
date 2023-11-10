using Isatays.FTGO.CustomerService.Core.Common.Exceptions;
using Isatays.FTGO.CustomerService.Core.Interfaces;
using KDS.Primitives.FluentResult;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;

namespace Isatays.FTGO.CustomerService.Core.Customers;

public class VerifyCustomerCommandHandler : IRequestHandler<VerifyCustomerCommand, Result<bool>>
{
	private readonly IDataContext _dataContext;
	private readonly ILogger<VerifyCustomerCommandHandler> _logger;
	private readonly ICustomerService _customerService;

	public VerifyCustomerCommandHandler(IDataContext dataContext, 
		ILogger<VerifyCustomerCommandHandler> logger,
		ICustomerService customerService)
	{
		_dataContext = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_customerService = customerService ?? throw new ArgumentNullException(nameof(customerService));
	}

	public async Task<Result<bool>> Handle(VerifyCustomerCommand request, CancellationToken cancellationToken)
	{
		var circuitBreaker = Policy.Handle<DatabaseException>().CircuitBreaker(3, TimeSpan.FromMinutes(1));
		Result<bool> result = null!;

		try
		{
			circuitBreaker.Execute(async () =>
				result = await _customerService.VerifyCustomer(request.Id, request.Name, request.Email)
			);
        }
		catch (DatabaseException ex)
		{
			_logger.LogError(ex.Message);
		}

        return result.Value;
    }
}
