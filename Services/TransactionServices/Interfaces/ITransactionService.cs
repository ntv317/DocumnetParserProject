using Database;
using Database.ModelFilter;
using Database.Models;
using Microsoft.AspNetCore.Http;

namespace Services.TransactionServices.Interfaces;

public interface ITransactionService
{
    Task ProcessData(IFormFile file);
    Task<PagedResult<object>> GetList(OperationFilter filter);


}