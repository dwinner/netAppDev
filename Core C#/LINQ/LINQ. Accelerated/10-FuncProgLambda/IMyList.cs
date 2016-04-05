namespace _10_FuncProgLambda
{
   /// <summary>
   /// Интерфейс однонаправленного связного списка в стиле Lisp
   /// </summary>
   public interface IMyList<out T>
   {
      /// <summary>
      /// Головной элемент
      /// </summary>
      T Head { get; }

      /// <summary>
      /// Хвост в виде остальной части списка
      /// </summary>
      IMyList<T> Tail { get; }
   }
}
