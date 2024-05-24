using Database.Models.Enums;
namespace Database.Models;

public class Operation
{
    public string Id { get; set; }
    public decimal Amount { get; set; }
    public string CurrencyCode { get; set; }
    public DateTime Date { get; set; }
    public OperationStatus Status { get; set; }
}