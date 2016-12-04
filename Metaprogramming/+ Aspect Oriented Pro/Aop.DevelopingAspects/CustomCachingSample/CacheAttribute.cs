namespace CustomCachingSample
{
   using System.Runtime.Caching;
   using System.Text;

   using Aop.Lib;

   using PostSharp.Aspects;
   using PostSharp.Serialization;

   /// <summary>
   ///    Пользовательский атрибут, когда применяется к методу, кэширует возвращаемое
   ///    значение метода, в зависимости от переданных значений параметров
   /// </summary>
   [PSerializable]
   public sealed class CacheAttribute : OnMethodBoundaryAspect
   {
      /// <summary>
      ///    Метод, который выполняется <i>перед</i> целевым методом аспекта
      /// </summary>
      /// <param name="args">Контекст выполнения метода</param>
      public override void OnEntry(MethodExecutionArgs args)
      {
         // Построим ключ кэша
         var stringBuilder = new StringBuilder();
         stringBuilder.AppendCallInformation(args);         
         string cacheKey = stringBuilder.ToString();

         // Получим значение из кэша
         object cachedValue = MemoryCache.Default.Get(cacheKey);
         if (cachedValue != null)
         {
            // Если значение уже было закешировано, не выполняем метод. Устанавливаем возвращаемое значение из кэша и немедленно выходим
            args.ReturnValue = cachedValue;
            args.FlowBehavior = FlowBehavior.Return;
         }
         else
         {
            // Если значение не закешировано, продолжаем выполение, но сохраняем ключ кеша для повторного использования
            args.MethodExecutionTag = cacheKey;
            args.FlowBehavior = FlowBehavior.Continue;
         }
      }

      /// <summary>
      ///    Метод, который выполняется <i>после</i> целевого метода аспекта
      /// </summary>
      /// <param name="args">Контекст выполнения метода</param>
      public override void OnSuccess(MethodExecutionArgs args)
      {
         var cacheKey = (string) args.MethodExecutionTag;
         MemoryCache.Default[cacheKey] = args.ReturnValue;
      }
   }
}