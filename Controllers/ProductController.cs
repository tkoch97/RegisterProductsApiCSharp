using Microsoft.AspNetCore.Mvc;
using RegisterProductsAPI.Mock;
using RegisterProductsAPI.Models;
using RegisterProductsAPI.Repository;
using RegisterProductsAPI.Services;
using RegisterProductsAPI.ViewModels;

namespace RegisterProductsApi.Controllers
{
  [Route("api/")]
  [ApiController]
  public class ProductController : ControllerBase
  {
    private readonly ProductService _service;

    public ProductController(ProductService service)
    {
      _service = service;
    }

    [HttpGet("products")]
    public async Task<IActionResult> GetAll()
    {

      try
      {
        var products = await _service.ListAllProducts();
        return Ok(products);
      }
      catch (Exception ex)
      {
        return NotFound(ex.Message);
      }
    }

    [HttpPost("products")]
    public async Task<IActionResult> PostProduct([FromBody] CreateProductViewModel newProductData)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        var newProductAdded = await _service.AddNewProduct(newProductData);
        return Created($"api/products/{newProductAdded.Id}", new
        {
          message = $"{newProductAdded.Name} adicinado com sucesso!"
        });
      }
      catch (Exception ex)
      {
        return BadRequest($"Não foi possível adicionar o produto: {ex.Message}");
      }
    }

    // [HttpGet("products/{id}")]
    // public IActionResult GetById([FromRoute] int id)
    // {
    //   var product = products.FirstOrDefault(p => p.Id == id);
    //   return product == null ? NotFound("Nenhum produto encontrado") : Ok(product);
    // }


    // [HttpDelete("products/{id}")]
    // public IActionResult DeleteById([FromRoute] int id)
    // {
    //   var productToDelete = products.FirstOrDefault(p => p.Id == id);
    //   if (productToDelete == null)
    //   {
    //     return NotFound("Produto não existente");
    //   }
    //   products.Remove(productToDelete);
    //   return Ok($"{productToDelete.Name} exluído(a) com sucesso");
    // }

    // [HttpPut("products/{id}")]
    // public IActionResult UpdateById(
    //   [FromRoute] int id,
    //   [FromBody] UpdateProductViewModel model
    //   )
    // {
    //   var productToUpdate = products.FirstOrDefault(p => p.Id == id);
    //   if (productToUpdate == null)
    //   {
    //     return NotFound("Não foi possível encontrar o produto desejado");
    //   }
    //   try
    //   {
    //     var updatedProduct = new Product(
    //       productToUpdate.Id,
    //       model.Name,
    //       model.Price,
    //       model.Stock
    //     );

    //     var indexOfProductToUpdate = products.IndexOf(productToUpdate);
    //     products[indexOfProductToUpdate] = updatedProduct;

    //     return Ok($"Produto atualizado com sucesso");

    //   }
    //   catch (Exception error)
    //   {
    //     return BadRequest($"Erro ao atualizar o produto. {error.Message}");
    //   }
    // }
  }
}