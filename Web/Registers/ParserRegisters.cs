using Database.Models.Enums;
using Services.ParserServices.Imps;
using Services.ParserServices.Interfaces;

namespace DocumnetParserProject.Registers;

public static class ParserRegisters
{
        public static void Registrate(this IServiceCollection services)
        {
                services.AddTransient<IParserFactory, ParserFactory>();
                services.AddKeyedTransient<IParserService, CsvParserService>(FileType.Csv);
                services.AddKeyedTransient<IParserService, XmlParserService>(FileType.Xml); 
        }
}