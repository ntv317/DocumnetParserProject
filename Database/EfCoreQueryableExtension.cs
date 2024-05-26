using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Database;

public static class EfCoreQueryableExtension
{
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, int, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }
        
        public static PagedResult<T> PagingBy<T>(this IQueryable<T> query, 
            int skip, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.PageSize = pageSize;
            result.RowCount = result.RecordsFiltered =  query.Count();
            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount); 
            result.Results = query.Skip(skip).Take(pageSize).ToList();
            return result;
        }
}
public abstract class PagedResultBase
{
    [JsonPropertyName("draw")]
    public int CurrentPage { get; set; } 
    public int PageCount { get; set; } 
    public int PageSize { get; set; } 
    [JsonPropertyName("recordsTotal")]
    public int RowCount { get; set; }
    [JsonPropertyName("recordsFiltered")]
    public int RecordsFiltered { get; set; }
 
    public int FirstRowOnPage
    {
 
        get { return (CurrentPage - 1) * PageSize + 1; }
    }
 
    public int LastRowOnPage
    {
        get { return Math.Min(CurrentPage * PageSize, RowCount); }
    }
}
 
public class PagedResult<T> : PagedResultBase where T : class
{
    [JsonPropertyName("data")]
    public IList<T> Results { get; set; }
 
    public PagedResult()
    {
        Results = new List<T>();
    }
}