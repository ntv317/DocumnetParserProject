using System.ComponentModel.DataAnnotations;
using Database;
using Database.ModelFilter;
using Database.Models;
using Database.Models.Enums;
using Microsoft.AspNetCore.Http;
using Services.FileServices.Interfaces;
using Services.TransactionServices.Interfaces;

namespace Services.TransactionServices.Imps;

public class TransactionService:ITransactionService
{
    private readonly IFileService _fileService;

    public TransactionService(IFileService fileService)
    {
        _fileService = fileService;
    }
    public async Task ProcessData(IFormFile file)
    {
        var operations = await _fileService.Parse(file);
        
        using (var db = new DocumentParserProjectDbContext())
        {
            await db.Operations.AddRangeAsync(operations);
 
            db.SaveChanges();
            var users = db.Operations.ToList();
            
        }

        throw new NotImplementedException();
    }

    public Task<PagedResult<object>> GetList(OperationFilter filter)
    {
        using (var db = new DocumentParserProjectDbContext())
        {
            var paged =  db.Operations
                .WhereIf(!string.IsNullOrEmpty(filter.CurrencyCode), x=>x.CurrencyCode.ToUpper() == filter.CurrencyCode.ToUpper())
                .WhereIf(filter.OperationStatus.HasValue, x=>x.Status == filter.OperationStatus)
                .WhereIf(filter.From.HasValue, x=>x.Date >= filter.From)
                .WhereIf(filter.To.HasValue, x=>x.Date < filter.To)
                .PagingBy(filter.start, filter.length);
            var list = paged.Results.Select(x => new
            {
                Id = x.Id,
                Payment = $"{x.Amount:0.00} {x.CurrencyCode}",
                Status = x.Status.GetDisplayName()
            }).ToArray();
            var result = new PagedResult<object>
            {
                CurrentPage = paged.CurrentPage,
                PageCount = paged.PageCount,
                RowCount = paged.RowCount,
                PageSize = paged.PageSize,
                RecordsFiltered = paged.RecordsFiltered,
                Results =  list 
            };
            return Task.FromResult(result);
        }
    }
}