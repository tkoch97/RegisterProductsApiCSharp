using RegisterProductsAPI.Models;
using RegisterProductsAPI.ViewModels;

namespace RegisterProductsAPI.Interfaces
{
  public interface IProductRepository
  {
    Task<List<Product>> GetAllProductsAsync();
    Task<Product> AddNewProductAsync(Product newProduct);
    Task<Product> DeleteProductAsync(int id);
    Task<Product> UpdateProductAsync(int id, UpdateProductViewModel dataToUpdateProduct);
    // Task<Product> GetProductByIdAsync(int id);
  }
}