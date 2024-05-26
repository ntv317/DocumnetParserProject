using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Services.FileServices.Interfaces;
using Services.ParserServices.Interfaces;

namespace Services.FileServices.Imps;

public class FileService:IFileService
{
    private readonly IParserFactory _parserFactory;

    public FileService(IParserFactory parserFactory)
    {
        _parserFactory = parserFactory;
    }
    public async Task<ICollection<Operation>> Parse(IFormFile formFile)
    {
        await Validate(formFile);
        var file = GetExtension(formFile);
        var parserService = _parserFactory.GetParserService(file);
        var res = await parserService.Parse(formFile);
        return res;
    }
    private async Task Validate(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
            throw new ArgumentException("File is empty");
        var extension = GetExtension(formFile);
        
        if (formFile.Length > 1024 * 1024) // 1MB = 1024 * 1024 bytes
        {
            throw new Exception("File is larger than 1MB.");
        }
    }
    
    private FileType GetExtension(IFormFile file)
    {
        string fileName = file.FileName;
        string fileExtension = Path.GetExtension(fileName);
        var extension = fileExtension.Substring(1);
        var canParse = Enum.TryParse<FileType>(extension,true,out var type);
        if (!canParse)
        {
            throw new Exception($"Extension {extension} is not supported");
        }
        return type;
    }
}