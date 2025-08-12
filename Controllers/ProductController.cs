using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization.Infrastructure;
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
        return BadRequest(new { message = $"Não foi possível adicionar o produto: {ex.Message}" });
      }
    }

    [HttpDelete("products/{id}")]
    public async Task<IActionResult> DeleteById([FromRoute] int id)
    {
      var DeletedProduct = await _service.DeleteProduct(id);
      if (DeletedProduct == null)
      {
        return NotFound(new { message = "Produto não existente" });
      }
      else
      {
        return Ok(new { message = $"{DeletedProduct.Name} excluído com sucesso" });
      }
    }

    [HttpPut("products/{id}")]
    public async Task<IActionResult> UpdateById(
      [FromRoute] int id,
      [FromBody] UpdateProductViewModel dataToUpdateProduct
      )
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }
      try
      {
        var updatedProduct = await _service.UpdateProduct(id, dataToUpdateProduct);
        return Ok(new
        {
          message = "Produto atualizado com sucesso!",
          product = updatedProduct
        });
      }
      catch (Exception error)
      {
        return BadRequest(new { message = $"Erro ao atualizar o produto. {error.Message}" });
      }
    }

    // [HttpGet("products/{id}")]
    // public IActionResult GetById([FromRoute] int id)
    // {
    //   var product = products.FirstOrDefault(p => p.Id == id);
    //   return product == null ? NotFound("Nenhum produto encontrado") : Ok(product);
    // }
  }
}