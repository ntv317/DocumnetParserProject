using Database.Models.Enums;
using Microsoft.Extensions.DependencyInjection;
using Services.ParserServices.Interfaces;

namespace Services.ParserServices.Imps;

public class ParserFactory:IParserFactory
{
    
    private readonly IServiceProvider _serviceProvider;

    public ParserFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public IParserService GetParserService(FileType fileType)
    {
        return _serviceProvider.GetRequiredKeyedService<IParserService>(fileType);
    }
}