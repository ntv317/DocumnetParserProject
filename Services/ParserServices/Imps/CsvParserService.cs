using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Services.ParserServices.Interfaces;

namespace Services.ParserServices.Imps;

public class CsvParserService:IParserService
{
    public async Task<ICollection<Operation>> Parse(IFormFile file)
    {
        throw new NotImplementedException();
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