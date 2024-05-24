using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;

namespace Services.ParserServices.Interfaces;

public interface IParserService
{
    Task<ICollection<Operation>> Parse(IFormFile file);
    OperationStatus ConvertStatus(string status);
}