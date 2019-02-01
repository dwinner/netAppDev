using System.Reflection;
using System.Runtime.ExceptionServices;

namespace GenericFactory
{
   /// <summary>
   ///    Единственно законный способ создания объектов семейства Product
   /// </summary>
   public static class ProductFactory
   {
      public static T Create<T>()
         where T : ProductBase, new()
      {
         try
         {
            var product = new T();

            // Вызываем постобработку
            product.PostConstruct();

            return product;
         }
         catch (TargetInvocationException e)
         {
            // "разворачиваем" исключение и бросаем исходное
            var exDispatchInfo = ExceptionDispatchInfo.Capture(e.InnerException);
            exDispatchInfo.Throw();
         }

         return default(T);
      }
   }
}