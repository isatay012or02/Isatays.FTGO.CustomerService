namespace Isatays.FTGO.CustomerService.Core.Entities;

public class DeliveryAddress
{
    public int AddressId { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}