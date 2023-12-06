namespace UsingMocks4;

internal class ProductRepository
{
   public Product GetById(int productId) => new();
}