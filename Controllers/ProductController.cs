using Microsoft.AspNetCore.Mvc;
using RegisterProductsAPI.Mock;

namespace RegisterProductsApi.Controllers
{
  [Route("api/")]
  [ApiController]
  public class ProductController : ControllerBase
  {

    [HttpGet("products")]
    public IActionResult GetAll()
    {
      return Ok(ProductsData.ProductsMock);
    }
  }
}