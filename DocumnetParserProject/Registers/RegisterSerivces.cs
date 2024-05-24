using Services.Common.Imps;
using Services.Common.Interfaces;
using Services.FileServices.Imps;
using Services.FileServices.Interfaces;

namespace DocumnetParserProject.Registers;

public static class RegisterSerivces
{
    public static void Registrate(this IServiceCollection services)
    {
        services.AddScoped<IValidateValueService, ValidateValueService>();
        services.AddScoped<IFileService, FileService>();
    }
}