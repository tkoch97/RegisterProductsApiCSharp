using RegisterProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RegisterProductsAPI.Data
{
  public class AppDbContext : DbContext
  {
    public DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
  }
}