using RegisterProductsAPI.Data;
using RegisterProductsAPI.Models;
using RegisterProductsAPI.Repository;

namespace RegisterProductsAPI.Services
{
  public class ProductService
  {
    private readonly AppDbContext _context;
    private readonly ProductsRepository _repository;

    public ProductService(AppDbContext context) //constructor
    {
      _context = context;
      _repository = new ProductsRepository();
    }
    public async Task<List<Product>> ListProductsAsync()
    {
      var products = await _repository.ListAllProducts(_context);
      if (products == null)
      {
        throw new Exception("Nenhum produto na lista");
      }
      return products;
    }
  }
}