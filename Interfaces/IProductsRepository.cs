using RegisterProductsAPI.Models;

namespace RegisterProductsAPI.Interfaces
{
  public interface IProductRepository
  {
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> AddNewProductAsync(Product newProduct);
    // Task<Product> GetProductByIdAsync(int id);
    // Task<> DeleteProductAsync(int id);
    // Task<dynamic> UpdateProductAsync(int id, Product updatedProduct);
  }
}