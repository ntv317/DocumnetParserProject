using Services.Common.Imps;
using Services.Common.Interfaces;
using Services.FileServices.Imps;
using Services.FileServices.Interfaces;
using Services.TransactionServices.Imps;
using Services.TransactionServices.Interfaces;

namespace DocumnetParserProject.Registers;

public static class RegisterSerivces
{
    public static void Registrate(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyService, CurrencyService>();
        services.AddScoped<IFileService, FileService>();
        services.AddScoped<ITransactionService, TransactionService>();
    }
}