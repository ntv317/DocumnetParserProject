using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Services.ParserServices.Interfaces;

namespace Services.ParserServices.Imps;

public class XmlParserService:IParserService
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
            case "Rejected":
                return OperationStatus.Rejected;
            case "Done":
                return OperationStatus.Done;
            default:
                throw new Exception($"Status :{status} is not supported");

            
        }
    }
}