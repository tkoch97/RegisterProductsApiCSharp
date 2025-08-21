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

      if (string.IsNullOrWhiteSpace(newProductData.Name))
      {
        throw new Exception("O título do produto é obrigatório");
      }

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

    public async Task<Product> UpdateProduct(int id, UpdateProductViewModel dataToUpdateProduct)
    {
      var product = await _repository.UpdateProductAsync(id, dataToUpdateProduct);
      if (product != null)
      {
        return product;
      }
      throw new Exception("Produto não encontrado para atualização.");
    }
  }
}