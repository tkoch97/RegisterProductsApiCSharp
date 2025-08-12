using Microsoft.EntityFrameworkCore;
using RegisterProductsAPI.Data;
using RegisterProductsAPI.Interfaces;
using RegisterProductsAPI.Models;

namespace RegisterProductsAPI.Repository
{
  public class ProductsRepository : IProductRepository
  {
    private readonly AppDbContext _context;

    public ProductsRepository(AppDbContext context)
    {
      _context = context;
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
      var products = await _context.Products.ToListAsync();
      return products;
    }

    public async Task<Product> AddNewProductAsync(Product newProduct)
    {
      await _context.Products.AddAsync(newProduct);
      await _context.SaveChangesAsync();
      return newProduct;
    }
  }
}