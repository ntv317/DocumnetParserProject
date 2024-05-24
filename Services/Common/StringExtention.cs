using System.Globalization;
namespace Services.Common;

public static class StringExtension
{
    public static decimal ParseToDecimal(this string amountSt)
    {
        if (decimal.TryParse(
                amountSt, 
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture, 
                out var amount))
        {
            return amount;
        }
        throw new Exception($"Format amount is not supported: {amountSt}");
    }
    
    public static DateTime ParseDate( this string dateString, string format)
    {
        DateTime date;
        var canParse = DateTime.TryParseExact(dateString, format, CultureInfo.InvariantCulture,DateTimeStyles.None, out date);
        if (!canParse)
        {
            throw new Exception($"Date string {dateString} not correct format {format}");
        }
        return date;
    }
}