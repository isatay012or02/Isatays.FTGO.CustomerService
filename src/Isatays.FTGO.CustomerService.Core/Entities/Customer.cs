using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isatays.FTGO.CustomerService.Core.Entities;

[Table("Customer")]
public class Customer
{
    [Key]
    public int CustomerId { get; set; }
    
    public string Name { get; set; }
    
    public string Email { get; set; }
    
    public string PhoneNumber { get; set; }
}
