using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isatays.FTGO.CustomerService.Core.Entities;

[Table("Order")]
public class Order
{
    [Key]
    public Guid Id { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("email")]
    public string Email { get; set; }
}
