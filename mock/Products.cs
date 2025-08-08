using RegisterProductsAPI.Models;

namespace RegisterProductsAPI.Mock
{
  public static class ProductsData
  {
    public static readonly List<Product> ProductsMock =
      [
        new Product(1, "Notebook", 5000, 10),
        new Product(2, "Mouse", 200, 20),
        new Product(3, "Boby Goods", 300, 10)
      ];
  }
}