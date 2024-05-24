using System.Globalization;
using Services.Common.Interfaces;

namespace Services.Common.Imps;

public class ValidateValueService:IValidateValueService
{
    public bool ValidateCurrency(string curr)
    {
        var symbol = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Where(c => !c.IsNeutralCulture)
            .Select(culture => {
                try{
                    return new RegionInfo(culture.Name);
                }
                catch
                {
                    return null;
                }
            })
            .Where(ri => ri!=null && ri.ISOCurrencySymbol.ToUpper() == curr.ToUpper())
            .Select(ri => ri.CurrencySymbol)
            .FirstOrDefault();
        return symbol != null;
    }
}