using Microsoft.AspNetCore.Mvc;
using RegisterProductsAPI.Mock;
using RegisterProductsAPI.Models;

namespace RegisterProductsApi.Controllers
{
  [Route("api/")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private static readonly List<Product> productsMock = ProductsData.ProductsMock;
    [HttpGet("products")]
    public IActionResult GetAll() => Ok(productsMock);

    [HttpGet("products/{id}")]
    public IActionResult GetById([FromRoute] int id)
    {
      var product = productsMock.FirstOrDefault(p => p.Id == id);
      return product == null ? NotFound("Nenhum produto encontrado") : Ok(product);
    }
  }
}