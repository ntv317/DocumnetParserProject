using Database.Models.Enums;

namespace Database.ModelFilter;

public class OperationFilter
{
    public string? CurrencyCode { get; set; }
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public OperationStatus? OperationStatus { get; set; }
    public int draw { get; set; }
    public int start { get; set; }
    public int length { get; set; }
}