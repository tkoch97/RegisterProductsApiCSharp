using RegisterProductsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace RegisterProductsAPI.Data
{
  public class AppDbContext : DbContext
  {

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    public DbSet<Product> Products { get; set; }

  }
}