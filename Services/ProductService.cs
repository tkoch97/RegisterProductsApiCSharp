using Microsoft.AspNetCore.Authorization.Infrastructure;
using RegisterProductsAPI.Data;
using RegisterProductsAPI.Interfaces;
using RegisterProductsAPI.Models;
using RegisterProductsAPI.ViewModels;

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

    public async Task<Product> AddNewProduct(CreateProductViewModel newProductData)
    {

      var newProduct = new Product
      {
        Name = newProductData.Name,
        Price = newProductData.Price,
        Stock = newProductData.Stock
      };

      return await _repository.AddNewProductAsync(newProduct);
    }

    public async Task<Product> DeleteProduct(int id)
    {
      return await _repository.DeleteProductAsync(id);
    }
  }
}