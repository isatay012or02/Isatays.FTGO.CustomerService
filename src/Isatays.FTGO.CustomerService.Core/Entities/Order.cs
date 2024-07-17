using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Isatays.FTGO.CustomerService.Core.Entities.Enums;

namespace Isatays.FTGO.CustomerService.Core.Entities;

[Table("Order", Schema = "public")]
public class Order
{
    [Column("id")]
    public Guid OrderId { get; set; }
    
    [Column("customerId")]
    public int CustomerId { get; set; }
    
    public Customer Customer { get; set; }
    
    [Column("date")]
    public DateTime OrderDate { get; set; }
    
    [Column("deliveryDate")]
    public DateTime? DeliveryDate { get; set; }
    
    public List<OrderItem> Items { get; set; } = new();
    
    [Column("status")]
    public OrderStatus Status { get; set; }
    public decimal TotalAmount { get; set; }
    public PaymentDetails Payment { get; set; }
    public DeliveryAddress DeliveryAddress { get; set; }
    public string Notes { get; set; }
}
