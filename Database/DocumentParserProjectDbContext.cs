using Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Database;

public partial class DocumentParserProjectDbContext: DbContext
{
    
    public DbSet<Operation> Operations => Set<Operation>();
    public DocumentParserProjectDbContext() => Database.EnsureCreated();
 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .SetBasePath(Directory.GetCurrentDirectory())
            .Build();
 
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
    }
}