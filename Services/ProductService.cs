using RegisterProductsAPI.Data;
using RegisterProductsAPI.Interfaces;
using RegisterProductsAPI.Models;

namespace RegisterProductsAPI.Services
{
  public class ProductService(IProductRepository _repository)
  {
    public async Task<List<Product>> ListAllProducts()
    {
      var products = await _repository.GetAllProductsAsync();
      if (products == null || products.Count == 0)
      {
        throw new Exception("Nenhum produto na lista");
      }
      return products;
    }
  }
}