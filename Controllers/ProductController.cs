using Microsoft.AspNetCore.Mvc;
using RegisterProductsAPI.Mock;
using RegisterProductsAPI.Models;
using RegisterProductsAPI.ViewModels;

namespace RegisterProductsApi.Controllers
{
  [Route("api/")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private static readonly List<Product> products = ProductsData.ProductsMock;

    [HttpGet("products")]
    public IActionResult GetAll() => Ok(products);

    [HttpGet("products/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
      var product = products.FirstOrDefault(p => p.Id == id);
      return product == null ? NotFound("Nenhum produto encontrado") : Ok(product);
    }

    [HttpPost("products")]
    public IActionResult Post([FromBody] CreateProductViewModel model)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      var newProduct = new Product
      (
        products.Count + 1,
        model.Name,
        model.Price,
        model.Stock
      );

      products.Add(newProduct);
      return Ok($"{newProduct.Name} adicionado(a) com sucesso");
    }

    [HttpDelete("products/{id}")]
    public IActionResult DeleteById([FromRoute] int id)
    {
      var productToDelete = products.FirstOrDefault(p => p.Id == id);
      if (productToDelete == null)
      {
        return NotFound("Produto não existente");
      }
      products.Remove(productToDelete);
      return Ok($"{productToDelete.Name} exluído(a) com sucesso");
    }
  }

}