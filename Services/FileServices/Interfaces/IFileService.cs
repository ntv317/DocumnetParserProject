using Database.Models;
using Microsoft.AspNetCore.Http;
namespace Services.FileServices.Interfaces;

public interface IFileService
{
    Task<ICollection<Operation>> Parse(IFormFile formFile);
}