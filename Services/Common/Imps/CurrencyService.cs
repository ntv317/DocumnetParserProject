using System.Globalization;
using Services.Common.Interfaces;

namespace Services.Common.Imps;

public class CurrencyService:ICurrencyService
{
    public bool ValidateCurrency(string curr)
    {
        var symbols = GetCurrencyIsoList();
        var symbol = symbols.Contains(curr.ToUpper());
        return symbol;
    }

    public HashSet<string> GetCurrencyIsoList()
    {
        var currencies = CultureInfo
            .GetCultures(CultureTypes.AllCultures)
            .Where(c => !c.IsNeutralCulture)
            .Select(culture => 
            {
                try
                {
                    return new RegionInfo(culture.Name);
                }
                catch
                {
                    return null;
                }
            })
            .Where(ri => ri!=null)
            .Select(ri => ri.ISOCurrencySymbol.ToUpper())
            .OrderDescending()
            .ToHashSet();
        return currencies;
    }

    public List<DropdownList<string>> GetDropdownList(string query)
    {
        return GetCurrencyIsoList()
                .AsEnumerable()
                .WhereIf(!string.IsNullOrEmpty(query),x=>x.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                .Select(x => new DropdownList<string>(){Id = x, Text = x})
                .ToList();
        
    }
}