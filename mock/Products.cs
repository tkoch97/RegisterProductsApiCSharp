using RegisterProductsAPI.Models;

namespace RegisterProductsAPI.Mock
{
  public static class ProductsData
  {
    public static readonly List<Product> ProductsMock =
      [
        new Product{
          Name = "Notebbok",
          Price = 5000,
          Stock = 10
        },
        new Product{
          Name = "Mouse",
          Price = 100,
          Stock = 15
        },
        new Product{
          Name = "Teclado",
          Price = 500,
          Stock = 20
        },
      ];
  }
}