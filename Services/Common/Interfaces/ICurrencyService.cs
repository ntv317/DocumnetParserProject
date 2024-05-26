namespace Services.Common.Interfaces;

public interface ICurrencyService
{
    bool ValidateCurrency(string curr);
    HashSet<string> GetCurrencyIsoList();
    List<DropdownList<string>> GetDropdownList(string query);
}