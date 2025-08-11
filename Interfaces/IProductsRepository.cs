using RegisterProductsAPI.Models;

namespace RegisterProductsAPI.Interfaces
{
  public interface IProductRepository
  {
    Task<List<Product>> GetAllProductsAsync();
    // Task<Product> GetProductByIdAsync(int id);
    // Task<dynamic> AddNewProductAsync(Product newProduct);
    // Task<dynamic> DeleteProductAsync(int id);
    // Task<dynamic> UpdateProductAsync(int id, Product updatedProduct);
  }
}