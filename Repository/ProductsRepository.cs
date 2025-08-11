using Microsoft.EntityFrameworkCore;
using RegisterProductsAPI.Data;
using RegisterProductsAPI.Models;

namespace RegisterProductsAPI.Repository
{
  public class ProductsRepository
  {
    public async Task<List<Product>> ListAllProducts(AppDbContext context)
    {
      var products = await context.Products.ToListAsync();
      return products;
    }
  }
}