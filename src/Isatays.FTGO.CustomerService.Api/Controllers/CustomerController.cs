using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Isatays.FTGO.CustomerService.Api.Controllers;

/// <summary>
/// Cotroller special for foods
/// </summary>
[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/consumer")]
[ApiVersion("1.0")]
public class CustomerController : BaseController
{
	private readonly ILogger<CustomerController> _logger;

	public CustomerController(ILogger<CustomerController> logger)
	{
		_logger = logger;
	}

	[HttpPost("check-consumer")]
	public async Task<IActionResult> CheckConsumer()
	{
		return Ok();
	}
}
