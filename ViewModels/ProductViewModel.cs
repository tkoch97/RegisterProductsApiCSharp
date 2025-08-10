using System.ComponentModel.DataAnnotations;

namespace RegisterProductsAPI.ViewModels
{
  public class CreateProductViewModel
  {
    [Required(ErrorMessage = "O nome do produto é obrigatório.")]
    [StringLength(65, ErrorMessage = "O nome do produto só pode ter no máximo 65 caracteres")]
    public string Name { get; set; } = String.Empty;

    [Required(ErrorMessage = "É necessário definir um preço para o produto.")]
    public decimal Price { get; set; }

    public int Stock { get; set; }
  }

  public class UpdateProductViewModel
  {
    public string Name { get; set; } = String.Empty;
    public decimal Price { get; set; }
    public int Stock { get; set; }
  }
}