namespace Decorator
{
   /// <summary>
   /// Интерфейс элемента проекта
   /// </summary>      
   public interface IProjectItem
   {
      /// <summary>
      /// Затраченное время
      /// </summary>
      double TimeRequired { get; set; }
   }
}
