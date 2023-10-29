using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Isatays.FTGO.CustomerService.Core.Entities;

[Table("Customer")]
public class Customer
{
    [Key]
    public Guid Id { get; private set; }

    [Column("name")]
    public string Name { get; private set; } = string.Empty;

    [Column("email")]
    public string Email { get; private set; } = string.Empty;

    [Column("isAvailable")]
    public bool IsAvailable { get; private set; }
}
