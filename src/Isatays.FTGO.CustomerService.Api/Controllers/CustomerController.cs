using Asp.Versioning;
using Isatays.FTGO.CustomerService.Api.Models;
using Isatays.FTGO.CustomerService.Core.Customers;
using Microsoft.AspNetCore.Mvc;

namespace Isatays.FTGO.CustomerService.Api.Controllers;

/// <summary>
/// Cotroller for Customers
/// </summary>
[Route("api/v{version:apiVersion}/customer")]
[ApiVersion("1.0")]
public class CustomerController : BaseController
{
	private readonly ILogger<CustomerController> _logger;

	public CustomerController(ILogger<CustomerController> logger)
	{
		_logger = logger;
	}

	[HttpPost("verify-customer/{id}")]
	public async Task<IActionResult> VerifyCustomer([FromBody] VerifyCustomerRequest request)
	{
		var scope = new Dictionary<string, object>
		{
			{ "Id", request.Id },
			{ "Name", request.Name },
			{ "Email", request.Email }
		};
		using (_logger.BeginScope(scope))
		{
            _logger.LogInformation("Запрос на проверку заказчик");
            var result = await Sender.Send(new VerifyCustomerCommand(request.Id, request.Name, request.Email));

            if (result.IsFailed)
                return ProblemResponse(result.Error);

            _logger.LogInformation("Заказчик успешно проверен");

            return Ok(result.Value);
        }
	}
}
