using Database.Models.Enums;

namespace Services.ParserServices.Interfaces;

public interface IParserFactory
{
    IParserService GetParserService(FileType fileType);
}