using Database.Models;
using Microsoft.AspNetCore.Http;
using Services.FileServices.Interfaces;
namespace Services.FileServices.Imps;

public class FileService:IFileService
{
    public async Task Validate(IFormFile formFile)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Operation>> Parse(IFormFile formFile)
    {
        throw new NotImplementedException();
    }
}