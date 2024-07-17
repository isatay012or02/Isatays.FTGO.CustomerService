namespace Isatays.FTGO.CustomerService.Core.Entities;

public class PaymentDetails
{
    public int PaymentId { get; set; }
    public string PaymentMethod { get; set; }
    public bool IsPaymentCompleted { get; set; }
    public DateTime PaymentDate { get; set; }
}