namespace Isatays.FTGO.CustomerService.Api.Models;

public record VerifyCustomerRequest(int Id, string Name, string Email, string PhoneNumber);
