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
    private readonly ILogger<ProductController> _logger;

    public ProductController(ProductService service, ILogger<ProductController> logger)
    {
      _service = service;
      _logger = logger;
    }

    /// <summary>
    /// Obter todos os produtos
    /// </summary>
    /// <returns>Coleção dos produtos registrados</returns>
    /// <response code="200">Sucesso</response>
    /// <response code="400">Erro com a solicitação.</response>
    [HttpGet("products")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
      try
      {
        _logger.LogInformation("Recebida requisição para buscar todos os produtos cadastrados.");
        var products = await _service.ListAllProducts();
        _logger.LogInformation("Retornados {Count} produtos ao cliente.", products.Count);
        return Ok(products);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao buscar todos os produtos cadastrados.");
        return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> PostProduct([FromBody] CreateProductViewModel newProductData)
    {
      if (string.IsNullOrWhiteSpace(newProductData.Name))
      {
        _logger.LogWarning("Requisição inválida: Está faltando o parâmetro name");
        return BadRequest("O nome do produto é obrigatório.");
      }
      try
      {
        _logger.LogInformation("Recebida requisição para adicionar o produto {name}", newProductData.Name);
        var newProductAdded = await _service.AddNewProduct(newProductData);
        _logger.LogInformation("{name} adicionado com sucesso", newProductData.Name);
        return Created($"api/products/{newProductAdded.Id}", new
        {
          message = $"{newProductAdded.Name} adicinado com sucesso!"
        });
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Erro ao adicionar o produto {name}", newProductData.Name);
        return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Não foi possível adicionar o produto: {ex.Message}" });
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
      _logger.LogInformation("Recebida a requisção para deletar o produto com de id {id}", id);
      var DeletedProduct = await _service.DeleteProduct(id);
      if (DeletedProduct == null)
      {
        _logger.LogWarning("Produto de {id} não encontrado", id);
        return NotFound(new { message = "Produto não existente" });
      }
      else
      {
        _logger.LogInformation("{name} excluído com sucesso", DeletedProduct.Name);
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
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        _logger.LogInformation("Recebida requisição para atualizar o produto de id {id}", id);
        var updatedProduct = await _service.UpdateProduct(id, dataToUpdateProduct);
        _logger.LogInformation("Produto atualizado com sucesso");
        return Ok(new
        {
          message = "Produto atualizado com sucesso!",
          product = updatedProduct
        });
      }
      catch (Exception ex)
      {
        _logger.LogWarning(ex, "Erro ao tentar atualizar o produto.");
        return StatusCode(StatusCodes.Status500InternalServerError, new { message = $"Erro ao atualizar o produto. {ex.Message}" });
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