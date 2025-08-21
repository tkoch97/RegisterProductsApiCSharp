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

    /// <summary>
    /// Obter todos os produtos
    /// </summary>
    /// <returns>Coleção dos produtos registrados</returns>
    /// <response code="200">Sucesso</response>
    /// <response code="400">Erro com a solicitação.</response>
    [HttpGet("products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {

      try
      {
        var products = await _service.ListAllProducts();
        return Ok(products);
      }
      catch (Exception ex)
      {
        return BadRequest(ex.Message);
      }
    }

    /// <summary>
    /// Cadastrar um novo produto.
    /// </summary>
    /// <remarks>O campo de título é obrigatório.</remarks>
    /// <param name="newProductData">Dados do novo produto.</param>
    /// <returns>Objeto JSON com mensagem de sucesso ou erro.</returns>
    /// <response code="201">Criado.</response>
    /// <response code="400">Erro com a solicitação.</response>
    [HttpPost("products")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    /// <summary>
    /// Deleta um produto.
    /// </summary>
    /// <param name="id">Identificador do produto.</param>
    /// <returns>Objeto JSON com mensagem de sucesso ou erro.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="404">Não encontrado.</response>
    [HttpDelete("products/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    /// <summary>
    /// Atualiza um produto.
    /// </summary>
    /// <remarks>{"name": "string", "price": 0,"stock": 0}</remarks>
    /// <param name="id">Identificador do produto.</param>
    /// <param name="dataToUpdateProduct">Dados do produto.</param>
    /// <returns>Objeto Json com mensagem de sucesso e produto atualizado ou menagem de erro.</returns>
    /// <response code="200">Sucesso.</response>
    /// <response code="400">Erro com a solicitação.</response>
    [HttpPut("products/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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