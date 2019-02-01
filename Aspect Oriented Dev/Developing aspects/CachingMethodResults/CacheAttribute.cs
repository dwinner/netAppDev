using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using PostSharp.Aspects;

namespace CachingMethodResults
{
   [Serializable]
   public sealed class CacheAttribute : OnMethodBoundaryAspect
   {
      private string _methodName;
      // Это поле будет установлено методом CompileTimeInitialize и сериализовано во время сборки, а десериализовано во время выполнения

      public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
      // Метод, который будет выполнен во время сборки
      {
         Debug.Assert(method.DeclaringType != null, "method.DeclaringType != null");
         _methodName = $"{method.DeclaringType.FullName}.{method.Name}";
      }

      private string GetCacheKey(object instance, Arguments arguments)
      {
         // Если у метода нет аргументов, мы просто возвращаем имя метода, поэтому можно не выделять память
         if (instance == null && arguments.Count == 0)
            return _methodName;

         // Добавим все аргументы в ключ кэша. Обратите внимание, что параметризованные аргументы не являются
         // частью кэша, поэтому вызовы метода, которые отличаются только обобщенным параметром будут приводить к конфликтам
         var stringBuilder = new StringBuilder(_methodName);
         stringBuilder.Append('(');
         if (instance != null)
         {
            stringBuilder.Append(instance);
            stringBuilder.Append("; ");
         }

         for (var i = 0; i < arguments.Count; i++)
         {
            stringBuilder.Append(arguments.GetArgument(i) ?? "null");
            stringBuilder.Append(", ");
         }

         return stringBuilder.ToString();
      }

      public override void OnEntry(MethodExecutionArgs args)
      // Этот метод выполняется перед выполнением метода назначения
      {
         // Вычисляем ключ кэша
         var cacheKey = GetCacheKey(args.Instance, args.Arguments);

         // Извлекаем значение из кэша
         var value = ExecutionCache.Impl[cacheKey];
         if (value != null) // Ok. Значение найдено в кэше. Не выполняем метод. Возвращаем значение сразу же
         {
            args.ReturnValue = value;
            args.FlowBehavior = FlowBehavior.Return;
         }
         else // Значение не найдено в кэше. Продолжаем выполнение метода, но сохраним ключ между вызовами аспектов
         {
            args.MethodExecutionTag = cacheKey;
         }
      }

      public override void OnSuccess(MethodExecutionArgs args)
      // Метод выполнился первый (и последний) раз с уникальным набором параметров
      {
         var cacheKey = (string)args.MethodExecutionTag;
         ExecutionCache.Impl[cacheKey] = args.ReturnValue;
      }

      private sealed class ExecutionCache
      {
         private static readonly Dictionary<string, object> StorageMap = new Dictionary<string, object>();

         private ExecutionCache()
         {
         }

         public static ExecutionCache Impl { get; } = new ExecutionCache();

         public object this[string index]
         {
            get
            {
               object cacheValue;
               return StorageMap.TryGetValue(index, out cacheValue) ? cacheValue : null;
            }
            set
            {
               if (!StorageMap.ContainsKey(index))
               {
                  StorageMap.Add(index, value);
               }
            }
         }
      }
   }
}