using Microsoft.AspNetCore.Authorization.Infrastructure;
using RegisterProductsAPI.Data;
using RegisterProductsAPI.Interfaces;
using RegisterProductsAPI.Models;
using RegisterProductsAPI.ViewModels;

namespace RegisterProductsAPI.Services
{
  public class ProductService(IProductRepository _repository,
  ILogger<ProductService> _logger)
  {
    public async Task<List<Product>> ListAllProducts()
    {
      var products = await _repository.GetAllProductsAsync();
      if (products == null || products.Count == 0)
      {
        _logger.LogInformation("Nenhum produto encontrado.");
        return [];
      }
      _logger.LogInformation("{Count} produtos encontrados.", products.Count);
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
      _logger.LogInformation("Tetando adicionar {name} ao banco de dados.", newProduct.Name);
      return await _repository.AddNewProductAsync(newProduct);
    }

    public async Task<Product> DeleteProduct(int id)
    {
      _logger.LogInformation("Tentando deletar o produto de id {id}", id);
      return await _repository.DeleteProductAsync(id);
    }

    public async Task<Product> UpdateProduct(int id, UpdateProductViewModel dataToUpdateProduct)
    {
      _logger.LogInformation("Tentando atualizar o produto de {id}", id);
      var product = await _repository.UpdateProductAsync(id, dataToUpdateProduct);
      if (product != null)
      {
        return product;
      }
      throw new Exception("Produto não encontrado para atualização.");
    }
  }
}