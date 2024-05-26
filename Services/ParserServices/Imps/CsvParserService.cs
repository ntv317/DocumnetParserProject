using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualBasic.FileIO;
using Services.Common;
using Services.Common.Interfaces;
using Services.ParserServices.Interfaces;

namespace Services.ParserServices.Imps;

public class CsvParserService:IParserService
{
    private readonly ICurrencyService _currencyService;

    public CsvParserService(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }
    public async Task<ICollection<Operation>> Parse(IFormFile file)
    {
        using (var parser = new TextFieldParser(file.OpenReadStream()) )
        {
            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            var transactions = new List<Operation>();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                if (fields != null && fields.Any(x => string.IsNullOrEmpty(x)))
                {
                    throw new Exception($"This row contain null. {fields}");
                }
                var id = fields[0];
                var amount = fields[1].ParseToDecimal();
                if (!_currencyService.ValidateCurrency(fields[2]))
                {
                    throw new Exception($"This raw contain not valid currency. {fields}");
                }
                var currency = fields[2].ToUpper();
                var date = fields[3].ParseDate("dd/MM/yyyy hh:mm:ss");
                var status = ConvertStatus(fields[4]);
                transactions.Add(new Operation()
                {
                    Id = id,
                    CurrencyCode = currency,
                    Amount = amount,
                    Date = date,
                    Status = status
                });
            } 
                
            return await Task.FromResult<ICollection<Operation>>(transactions);
        }
    }

    public OperationStatus ConvertStatus(string status)
    {
        switch (status)
        {
            case "Approved":
                return OperationStatus.Approved;
            case "Failed":
                return OperationStatus.Rejected;
            case "Finished":
                return OperationStatus.Done;
            default:
                throw new Exception($"Status :{status} is not supported");
            
        }
    }
}