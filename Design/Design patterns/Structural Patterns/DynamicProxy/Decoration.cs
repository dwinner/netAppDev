using System;

namespace DynamicProxy
{
   /// <summary>
   ///    Тип для логики методов-декораторов
   /// </summary>
   public class Decoration
   {
      /// <summary>
      ///    Конструктор метода-декоратора
      /// </summary>
      /// <param name="decoratedAction">Метод</param>
      /// <param name="parameters">Параметры</param>
      public Decoration(Action<object, object[]> decoratedAction, object[] parameters = null)
      {
         Action = decoratedAction;
         Parameters = parameters;
      }

      /// <summary>
      ///    Метод
      /// </summary>
      public Action<object, object[]> Action { get; private set; }

      /// <summary>
      ///    Параметры
      /// </summary>
      public object[] Parameters { get; private set; }
   }
}